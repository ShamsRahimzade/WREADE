const container = document.getElementById('container');
const registerBtn = document.getElementById('register');
const loginBtn = document.getElementById('login');

registerBtn.addEventListener('click', () => {
    container.classList.add("active");
});

loginBtn.addEventListener('click', () => {
    container.classList.remove("active");
});


    
    function showPassword() {
        var passwordInput = document.getElementById("passwordInput");
    passwordInput.type = "text";
    }

    function hidePassword() {
        var passwordInput = document.getElementById("passwordInput");
    passwordInput.type = "password";
    }

    const passwordInput = document.getElementById("passwordInput");
    const showPasswordButton = document.getElementById("showPassword");

    showPasswordButton.addEventListener("click", function () {
        if (passwordInput.type === "password") {
        showPassword();
    showPasswordButton.classList.remove("fa-eye");
    showPasswordButton.classList.add("fa-eye-slash");
        } else {
        hidePassword();
    showPasswordButton.classList.remove("fa-eye-slash");
    showPasswordButton.classList.add("fa-eye");
        }
    });

