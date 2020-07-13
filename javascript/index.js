
function loadhr() {
    document.getElementById('hr-div').style.width = "100%";
}
function hideloadhr() {
        document.getElementById('hr-div').style.display = "none"; 
}


var i = 1;
var slid1 = document.getElementById('slid1');
var slid2 = document.getElementById('slid2');
var slid3 = document.getElementById('slid3');
var slid4 = document.getElementById('slid4');
var slid5 = document.getElementById('slid5');
var slid6 = document.getElementById('slid6');
var txt = document.getElementById('txtslid2');

//the slide of pictures and text
function mainslide() {

    if (i == 1) {
        slid1.style.display = "block";
        slid2.style.display = "none";
        slid3.style.display = "none";
        slid4.style.display = "none";
        slid5.style.display = "none";
        slid6.style.display = "none";
        txt.innerText = "Enjoy your time!!";

    }
        if (i == 2) {

            slid2.style.display = "block";
            slid1.style.display = "none";
            slid3.style.display = "none";
            slid4.style.displa = "none";
            slid5.style.display = "none";
            slid6.style.display = "none";
            txt.innerText = "Grow your knowledge";
        }
    
    
    if (i == 3) {

        slid3.style.display = "block";
        slid2.style.display = "none";
        slid1.style.display = "none";
        slid4.style.display = "none";
        slid5.style.display = "none";
        slid6.style.display = "none";

        txt.innerText = "Be heigher";
    }
    if (i == 4) {

        slid4.style.display = "block";
        slid3.style.display = "none";
        slid1.style.display = "none";
        slid2.style.display = "none";
        slid5.style.display = "none";
        slid6.style.display = "none";
        txt.innerText = "Visit the world";
    }
    if (i == 5) {

        slid5.style.display = "block";
        slid3.style.display = "none";
        slid1.style.display = "none";
        slid2.style.display = "none";
        slid4.style.display = "none";
        slid6.style.display = "none";

        txt.innerText = "Let the nature tell you..";
    }
    if (i == 6) {

        slid6.style.display = "block";
        slid3.style.display = "none";
        slid1.style.display = "none";
        slid2.style.display = "none";
        slid4.style.display = "none";
        slid5.style.display = "none";

        txt.innerText = "Take your first steps";
    }

    i++;
    if (i == 7) {
        i = 1;
    }
    
}

var change1 = document.getElementById('img-chang1');
var change2 = document.getElementById('img-chang2');
var change3 = document.getElementById('img-chang3');

//the movement of pictures
function movepic() {

    change1.style.marginTop = "300px";
    change2.style.margin = "300px";
    change3.style.margin = "300px";

    change1.style.marginLeft = "100px";
    change2.style.marginLeft = "500px";
    change3.style.marginLeft = "900px";

}


var j = 1;
var txt1 = document.getElementById('txt1');
var txt2 = document.getElementById('txt2');
var txt3 = document.getElementById('txt3');
var txt4 = document.getElementById('txt4');
var txt5 = document.getElementById('txt5');
var txt6 = document.getElementById('txt6');





///////



