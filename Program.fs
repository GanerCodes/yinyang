open System
open System.Security.Cryptography
open System.Net.Http

module Pomorot =
    let pomoendpoint = "/pomo"
    let rotendpoint = "/rot"
    let breaktime = string (5*60)                //https://en.wikipedia.org/wiki/Pomodoro_Technique; 5m break
    let worktime = 25*60
    let brainrot =
        $"<div align=\"center\" hx-get=\"{pomoendpoint} hx-trigger=\"load delay:{breaktime}\" hx-swap=\"outerHTML\"><iframe id=\"brainrot\" src=\"https://www.nyan.cat/\" title=\"Brainrot!\" style=\"position:fixed ; top:0 ; left:0 ; bottom:0 ; right:0 ; width:100%% ; height:100%% ; border:none ; margin:0 ; padding:0 ; overflow:hidden ; z-index:999999 ;\">Your browser doesn't support iframes, back to work!</iframe></div>"
    let work =
        let clocktime = DateTime.UtcNow.AddMinutes(worktime).ToString("HH:mm")
        let strworktime = string worktime
        $"<div align=\"center\" hx-get=\"{rotendpoint}\" hx-trigger=\"load delay:{strworktime}\" hx-swap=\"outerHTML\"><h1>Working until {clocktime}...</h1></div>"
    let htmxsrc = "<script src=\"https://unpkg.com/htmx.org@2.0.4\" integrity=\"sha384-HGfztofotfshcF7+8n44JQL2oJmowVChPTg48S+jvZoztPfvwD79OC/LTtG6dMp+\" crossorigin=\"anonymous\"></script>"
    // let htmx endpoint = $"hx-get=\"{endpoint}\" hx-trigger=\"[???]\" hx-swap=\"outerHTML\""
    // regular page: big thing that says: work til {time}
    // brainrot page: nyancat
    // One call to start a timer (returns a piece of JS that sleeps for N seconds, then makes a request to pull the TikTok)
    // setTimeout(lambda goes here, t) where t is in mills
    // One call to return a div that covers the whole screen, plays embedded TikTok (or Nyancat) for N seconds
    // maybe a usually-invisible div whose properties are modifiable
    

module Passgen =
    // One call to generate a password
    // an array of 100 random bytes hashed w/ SHA256 & returned as a hex string
    let genpass =
        let buf = new Span<byte>()
        (new Random()).NextBytes(buf)
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

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"
