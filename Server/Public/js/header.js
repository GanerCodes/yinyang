var print = (...𝔸)=>console.log(...𝔸)||𝔸.length&&𝔸[0];
var sleep = s=>new Promise(r=>setTimeout(r,1000*s));
var api = async (M,d,is_j=false) => {
    const r = await fetch("", { "method": "POST",
		                        "body": JSON.stringify({M:M,...d}) });
    const v = await (is_j?r.json():r.text());
    print(`API with "${JSON.stringify(d,null,"  ")}" → "${JSON.stringify(v,null,"  ")}" (status=${r.status})`);
    return v; };
var GID = (x,ε=document)=>ε.getElementById(x);
var QS  = (x,ε=document)=>ε.querySelector(x);
var QSA = (x,ε=document)=>[...ε.querySelectorAll(x)];
var wrap_submit = (ε,ƒ,ꬶ) => {
	ε.onclick = _ => {
		ε.disabled = true;
		ƒ().then(x => {
				ε.disabled = false;
				ꬶ(x); })
		   .catch(_ => {
				ε.disabled = false; }); } }