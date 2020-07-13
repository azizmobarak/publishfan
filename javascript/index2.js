

///reviews animation 

var div = document.getElementById('divreviews');
var reviw1 = document.getElementById('review1');
var reviw2 = document.getElementById('review2');
var reviw3 = document.getElementById('review3');
var u = 1;
function review() {
    if (u == 1) {
        reviw3.style.width = "800px";
        reviw1.style.width = "1000px";
        reviw2.style.width = "800px";
    }
    if (u == 2) {
        reviw1.style.width = "800px";
        reviw2.style.width = "1000px";
        reviw3.style.width = "800px";
    }
    if (u == 3) {
        reviw1.style.width = "800px";
        reviw2.style.width = "800px";
        reviw3.style.width = "1000px";
    }
    u++;
    if (u == 4) {
        u = 1;
    }
}