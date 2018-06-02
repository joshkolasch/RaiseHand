
var allLocations = document.getElementsByClassName('location');
for (var i = 0; i < allLocations.length; i++) {
    allLocations[i].addEventListener('click', locationClickEvent);
}
var locationDropdown = document.getElementById('LocationId');
locationDropdown.addEventListener('change', dropdownChangeEvent);

function testFunction1() {
    this.style.fill = 'red';
}

function testFunction2() {
    document.getElementById('demo-box').innerHTML = this.value;
}

function grayAllLocations() {
    for (var i = 0; i < allLocations.length; i++) {
        allLocations[i].style.fill = '#d3d3d3';
    }
}

function locationClickEvent() {
    grayAllLocations();
    this.style.fill = 'red';
    locationDropdown.value = this.id;
}

function dropdownChangeEvent() {
    grayAllLocations();
    var currentLocation = document.getElementById(this.value);
    currentLocation.style.fill = 'red';
}
