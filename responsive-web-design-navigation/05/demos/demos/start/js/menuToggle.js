function toggleMenu() {
    let y = document.getElementById('primaryNav');

    if(y.className==="closed"){
        ;y.className = "open"
    } else {
        y.className = "closed";
    }
}

let x = document.getElementById('hamburgerBtn');
x.onclick = toggleMenu;
