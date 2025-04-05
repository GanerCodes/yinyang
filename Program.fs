open System
open System.Security.Cryptography
open System.Net.Http

module Pomotok =
    let endpoint = "/pomotok"
    // One call to start a timer (returns a piece of JS that sleeps for N seconds, then makes a request to pull the TikTok)
    // setTimeout(lambda goes here, t) where t is in mills
    // One call to return a div that covers the whole screen, plays embedded TikTok (or Nyancat) for N seconds
    // maybe a usually-invisble div whose properties are modifiable

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

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"
printfn "%A" Passgen.genpass
