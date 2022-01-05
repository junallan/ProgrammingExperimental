//Create a button and give it an ID of "hamburgerBtn"
//Attach an ID of "primaryNav" to the UL of the unordered list that holds your links
function toggleMenu() {
	var y=document.getElementById("primaryNav");
	if (y.className==="closed") {
		y.className="open";
	} else {
		y.className="closed";
	} // end if
} // end function

let x = document.getElementById('hamburgerBtn');
x.onclick = toggleMenu; 