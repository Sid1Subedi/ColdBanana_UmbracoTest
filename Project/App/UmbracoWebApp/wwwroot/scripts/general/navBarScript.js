$(document).ready(function () {

    var contactButtonElem = document.getElementById("navBarContactBtn");

    // Add a click event listener to the button
    contactButtonElem.addEventListener("click", function () {
        // Get a reference to the section you want to navigate to
        var sectionToNavigate = document.getElementById("contactForm");
        sectionToNavigate.scrollIntoView({ behavior: "smooth" });
    });
});