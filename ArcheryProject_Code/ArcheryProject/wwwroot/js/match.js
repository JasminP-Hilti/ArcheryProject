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

    // Pass the id to modalSave function
    document.getElementById('saveButton').onclick = function () {
        modalSave(id);
    };
    //let element = document.getElementById(id);
    //if (element) {
    //    element.innerHTML = "clicked";
    //}
}
function closeModal() {
    // Hide the modal using Bootstrap's modal method
    $('#pointsModal').modal('hide');
}

function getSelectedValue(name) {
    var radios = document.getElementsByName(name);

    for (var i = 0; i < radios.length; i++) {
        if (radios[i].checked) {
            return radios[i].value;
        }
    }

    return null; // Return null if no radio button is selected
}
function modalSave(id) {
    // Get the selected values for each arrow
    const arrow1 = parseInt(getSelectedValue('A1')) || 0;
    const arrow2 = parseInt(getSelectedValue('A2')) || 0;
    const arrow3 = parseInt(getSelectedValue('A3')) || 0;

    const sum = arrow1 + arrow2 + arrow3;

    // Update the content of the clicked element with the sum
    var element = document.getElementById(id);
    element.innerHTML = sum;

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



function sumColumns() {
    // Get the table by ID
    var table = document.getElementById('matchTable');

    // Initialize an array to store the column sums
    var columnSums = [];

    // Loop through each column, excluding the first column
    for (var i = 1; i < table.rows[0].cells.length; i++) {
        // Initialize the sum for each column
        var sum = 0;

        // Loop through each row and accumulate the values in the current column
        for (var j = 0; j < table.rows.length; j++) {
            var cellValue = parseInt(table.rows[j].cells[i].innerHTML) || 0; // Parse cell content as integer
            sum += cellValue;
        }

        // Add the sum to the array
        columnSums.push(sum);
    }

    // Update the last cell in each column with the calculated sum
    for (var i = 1; i < table.rows[0].cells.length; i++) {
        table.rows[table.rows.length - 1].cells[i].innerHTML = columnSums[i - 1];
    }
}






