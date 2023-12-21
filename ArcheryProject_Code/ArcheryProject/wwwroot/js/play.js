document.addEventListener("DOMContentLoaded", () => {

    var addPlayerModal = document.getElementById('addPlayerModal');
    var myInput = document.getElementById('myInput')

    addPlayerModal.addEventListener('shown.bs.modal', function () {
        myInput.focus()
    })

    //matchAddPlayer
    //matchPlayerTable
}
