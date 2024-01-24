// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener("DOMContentLoaded", () => {
    document.getElementById("loginButton").addEventListener("click", switchForm);
    document.getElementById("registerButton").addEventListener("click", switchForm);

  
});



function switchForm(event) {
    const clickedButton = event.target;
    console.log(clickedButton.id)
    if (clickedButton.classList.contains("btn-inactive") == true) {
        let activeButton = clickedButton.id === 'loginButton' ? document.getElementById("registerButton") : document.getElementById("loginButton");

        activeButton.classList.remove("btn-active");
        activeButton.classList.add("btn-inactive");

        clickedButton.classList.remove("btn-inactive");
        clickedButton.classList.add("btn-active");

        const showFormId = clickedButton.id === 'loginButton' ? 'loginForm' : 'registerForm';
        const hideFormId = clickedButton.id === 'loginButton' ? 'registerForm' : 'loginForm';
        showForm(showFormId, hideFormId);
    }
}
function showForm(showId, hideId) {
    console.log("show " + showId + "hide " + hideId);
    document.getElementById(showId).style.display = 'block';
    document.getElementById(hideId).style.display = 'none';
}

