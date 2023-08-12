$(document).ready(function () {

    var fullNameInput = document.getElementById("fullName");
    var emailInput = document.getElementById("email");
    var messageInput = document.getElementById("message");
    var formSubmitBtn = document.getElementById("formSubmitBtn");
    var fullNameError = document.getElementById("fullNameError");
    var emailError = document.getElementById("emailError");
    var messageError = document.getElementById("messageError");

    // Input event listeners for real-time validation
    fullNameInput.addEventListener("input", validateFullName);
    emailInput.addEventListener("input", validateEmail);
    messageInput.addEventListener("input", validateMessage);

    formSubmitBtn.addEventListener("click", function () {
        var isFormValid = true;

        if (fullNameInput.value.trim().length < 1) {
            fullNameError.textContent = "Full Name is required";
            isFormValid = false;
        } else {
            fullNameError.textContent = "";
        }

        if (!isValidEmail(emailInput.value)) {
            emailError.textContent = "Invalid Email";
            isFormValid = false;
        } else {
            emailError.textContent = "";
        }

        if (messageInput.value.trim().length < 10) {
            messageError.textContent = "Message must be at least 10 characters";
            isFormValid = false;
        } else {
            messageError.textContent = "";
        }

        if (!isFormValid) {
            return;
        }

        // If form is valid, proceed with submission
        // Your submission logic here
        sendEmail({
            emailElem: emailInput,
            fullNameElem: fullNameInput,
            messageElem: messageInput,
        });
    });

    function validateFullName() {
        var fullName = fullNameInput.value.trim();
        var fullNameParts = fullName.split(" ");

        if (fullNameParts.length < 2) {
            fullNameError.textContent = "Please enter both first name and last name";
            return false;
        } else {
            fullNameError.textContent = "";
            return true;
        }
    }

    function validateEmail() {
        if (!isValidEmail(emailInput.value)) {
            emailError.textContent = "Invalid Email";
            return false;
        } else {
            emailError.textContent = "";
            return true;
        }
    }

    function validateMessage() {
        if (messageInput.value.trim().length < 10) {
            messageError.textContent = "Message must be at least 10 characters";
            return false;
        } else {
            messageError.textContent = "";
            return true;
        }
    }

    // Function to validate email
    function isValidEmail(email) {
        var emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        return emailRegex.test(email);
    }

    function sendEmail({ emailElem, fullNameElem, messageElem } = {}) {

        // Show loading indicator
        showLoadingIndicator();

        var requestData = {
            email: emailElem.value,
            fullName: fullNameElem.value,
            message: messageElem.value,
        };

        fetch('/umbraco/surface/SendEmail/SendByUmbraco', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.getElementsByName('__RequestVerificationToken')[0].value
            },
            body: JSON.stringify(requestData)
        })
            .then(response => response.json())
            .then(data => {
                if (data.errCode == "1") {
                    showAlertDialog({
                        icon: 'success',
                        title: 'success',
                        textMsg: data.errMsg
                    });
                }
                else {
                    showAlertDialog({
                        icon: 'warning',
                        title: 'warning',
                        textMsg: data.errMsg
                    });
                }
                // Clear form fields after successful submission
                fullNameElem.value = '';
                emailElem.value = '';
                messageElem.value = '';

                // Hide loading indicator
                hideLoadingIndicator();
            })
            .catch(error => {
                showAlertDialog({
                    icon: 'error',
                    title: 'error',
                    textMsg: 'Something Went Wrong. Please Try Again!'
                });

                // Hide loading indicator
                hideLoadingIndicator();
            });
    }
});