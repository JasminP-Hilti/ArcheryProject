// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener("DOMContentLoaded", () => {
    
    let matchtable = document.getElementById("matchDiv");
    matchtable.classList.add('disabled');
    
});


function openPointModal(id) {
    // Show the modal using Bootstrap's modal method
    $('#pointsModal').modal('show');

    //let element = document.getElementById(id);
    //if (element) {
    //    element.innerHTML = "clicked";
    //}
}
function closeModal() {
    // Hide the modal using Bootstrap's modal method
    $('#pointsModal').modal('hide');
}

function modalSave() {
    // Get the selected values for each arrow
    const arrow1 = getSelectedValue('A1');
    const arrow2 = getSelectedValue('A2');
    const arrow3 = getSelectedValue('A3');


    // Close the modal after processing the button click
    closeModal();
}



// Close the modal if the overlay outside the modal is clicked
window.onclick = function (event) {
    if (event.target === modal) {
        closeModal();
    }
};





function startMatch() {
    let matchtable = document.getElementById("matchDiv");
    matchtable.classList.remove('disabled');
    let setupMatch = document.getElementById('setupMatch');
    setupMatch.style.display = "none";

}
















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

