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
    var passwordInput = document.getElementById("password");
    passwordInput.type = "text";
}

function hidePassword() {
    var passwordInput = document.getElementById("password");
    passwordInput.type = "password";
}
function showPasssword() {
    var passwordInput = document.getElementById("passsword");
    passwordInput.type = "text";
}

function hidePasssword() {
    var passwordInput = document.getElementById("passsword");
    passwordInput.type = "password";
}
  