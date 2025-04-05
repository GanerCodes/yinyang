var print = (...𝔸) => console.log(...𝔸) || 𝔸.length && 𝔸[0];
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

// add classes for mobile navigation toggling
var CSbody = document.querySelector('body');
const CSnavbarMenu = document.querySelector('#cs-navigation');
const CShamburgerMenu = document.querySelector('#cs-navigation .cs-toggle');

CShamburgerMenu.addEventListener('click', function () {
	CShamburgerMenu.classList.toggle('cs-active');
	CSnavbarMenu.classList.toggle('cs-active');
	CSbody.classList.toggle('cs-open');
	// run the function to check the aria-expanded value
	ariaExpanded();
});

// checks the value of aria expanded on the cs-ul and changes it accordingly whether it is expanded or not
function ariaExpanded() {
	const csUL = document.querySelector('#cs-expanded');
	const csExpanded = csUL.getAttribute('aria-expanded');

	if (csExpanded === 'false') {
		csUL.setAttribute('aria-expanded', 'true');
	} else {
		csUL.setAttribute('aria-expanded', 'false');
	}
}

// mobile nav toggle code
const dropDowns = Array.from(document.querySelectorAll('#cs-navigation .cs-dropdown'));
for (const item of dropDowns) {
	const onClick = () => {
		item.classList.toggle('cs-active');
	};
	item.addEventListener('click', onClick);
}