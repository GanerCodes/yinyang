open System
open System.Security.Cryptography
open System.Net.Http

module Calmandchaos =
    // like pomorot, but switching back every 8 (auspicious or w/e) minutes; calm playing rain sounds & showing a picture of a painting or smth & chaos doing black metal & gunfire
    let calmendpoint = "/calm"
    let chaosendpoint = "/chaos"
    let calmaudsrc = "/calm.mp4"
    let chaosaudsrc = "/chaos.mp4"
    let calmimgsrc = "/calm.FILEXT"
    let chaosimgsrc = "/chaos.FILEXT"
    let waittime = string (8*60)        //https://en.wikipedia.org/wiki/Bagua
    let format endpoint audio image =
        $"<div align=\"center\" hx-get=\"{endpoint}\" hx-trigger=\"load delay:{waittime}\" hx-swap=\"outerHTML\"><image src=\"{image}\" /><audio src=\"{audio}\" autoplay=\"true\" loop=\"true\"></audio></div>"
    let calm = format chaosendpoint calmaudsrc calmimgsrc
    let chaos = format calmendpoint chaosaudsrc chaosimgsrc

module Pomorot =
    // regular page: big thing that says: work til {time}
    // brainrot page: nyancat
    let pomoendpoint = "/pomo"
    let rotendpoint = "/rot"
    let breaktime = string (5*60)                //https://en.wikipedia.org/wiki/Pomodoro_Technique; 5m break
    let worktime = 25*60
    let brainrot =
        $"<div align=\"center\" hx-get=\"{pomoendpoint}\" hx-trigger=\"load delay:{breaktime}\" hx-swap=\"outerHTML\"><iframe id=\"brainrot\" src=\"https://www.nyan.cat/\" title=\"Brainrot!\" style=\"position:fixed ; top:0 ; left:0 ; bottom:0 ; right:0 ; width:100%% ; height:100%% ; border:none ; margin:0 ; padding:0 ; overflow:hidden ; z-index:999999 ;\">Your browser doesn't support iframes, back to work!</iframe></div>"
    let work =
        let clocktime = DateTime.UtcNow.AddMinutes(worktime).ToString("HH:mm")
        let strworktime = string worktime
        $"<div align=\"center\" hx-get=\"{rotendpoint}\" hx-trigger=\"load delay:{strworktime}\" hx-swap=\"outerHTML\"><h1>Working until {clocktime}...</h1></div>"
    let htmxsrc = "<script src=\"https://unpkg.com/htmx.org@2.0.4\" integrity=\"sha384-HGfztofotfshcF7+8n44JQL2oJmowVChPTg48S+jvZoztPfvwD79OC/LTtG6dMp+\" crossorigin=\"anonymous\"></script>"  

module Passgen =
    // One call to generate a password
    // an array of 100 random bytes hashed w/ SHA256 & returned as a hex string
    let genpass =
        let buf: byte [] = Array.zeroCreate 100
        (new Random(DateTime.UtcNow.Millisecond * DateTime.UtcNow.Microsecond)).NextBytes(buf) // different seeds!
        SHA256.HashData(buf) |> Convert.ToHexString

    // Internal functions to send it to a pastebin
    // pastebin API: https://pastebin.com/doc_api
    let pastebin key x =
        let endpoint = "https://pastebin.com/api/api_post.php"
        let client = new HttpClient()
        let headers = new Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded")
        let content = new StringContent($"api_dev_key={key}&api_paste_code={x}&api_option=paste", headers)
        client.PostAsync(endpoint, content).Result

    let run key =
        let pass = genpass
        pastebin key pass |> ignore
        pass

[<EntryPoint>]
let main args =
    match args[0] with
        | "pass" -> printfn "%A" (Passgen.run args[1]) //have to pass it the key as well
        | "pomo" -> printfn "%A" Pomorot.work
        | "rot" -> printfn "%A" Pomorot.brainrot
        | "matrix" -> printfn "%A" (Matrices.getmatrix (int args[1])) //pass matrix index
        | "calm" -> printfn "%A" Calmandchaos.calm
        | "chaos" -> printfn "%A" Calmandchaos.chaos
        | _ -> printfn $"Error: unrecognized command {args[0]}"
    0
