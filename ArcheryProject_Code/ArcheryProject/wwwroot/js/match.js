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
   
    $('input[type="radio"]').prop('checked', false);
    
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

   
  

    sumColumns(id);
    closeModal();
}



function startMatch() {
    let matchtable = document.getElementById("matchDiv");
    matchtable.classList.remove('disabled');
    let setupMatch = document.getElementById('setupMatch');
    setupMatch.style.display = "none";

}


if (typeof window.playerList === 'undefined') {
    window.playerList = playerList;
}

function sumColumns(id) {
    
    for (var i = 0; i < playerList.length; i++) {
        var player = playerList[i];
        var sum = 0;
        $('[id^="' + player + '"]').each(function () {
            var value = parseInt($(this).text()) || 0;
            sum += value;
        });
        // Update the sum in the corresponding cell
        $('#sum-' + player).text(sum);

        // Update the hidden input value for the current player
        var hiddenInput = document.getElementById('points-' + player);
        hiddenInput.value = sum;
    }
    
}
document.addEventListener('DOMContentLoaded', function () {
    var scoreRadios = document.querySelectorAll('.score-radio');
    var closeButton = document.getElementById('closeButton');
    var saveButton = document.getElementById('saveButton');

    scoreRadios.forEach(function (radio) {
        radio.addEventListener('change', function () {
            handleRadioChange(radio);
        });
    });

    closeButton.addEventListener('click', resetRadioButtons);
    saveButton.addEventListener('click', resetRadioButtons);

    function handleRadioChange(selectedRadio) {
        if (selectedRadio.value !== '0') {
            scoreRadios.forEach(function (radio) {
                if (radio.value !== selectedRadio.value) {
                    radio.disabled = true;
                }
            });
        } else {
            resetRadioButtons();
        }
    }

    function resetRadioButtons() {
        scoreRadios.forEach(function (radio) {
            radio.disabled = false;
        });
    }
});

