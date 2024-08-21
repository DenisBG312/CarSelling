
// Open the modal and display the clicked image
function openModal(imageSrc) {
    var modal = document.getElementById("imageModal");
    var modalImg = document.getElementById("modalImage");
    var captionText = document.getElementById("caption");

    modal.style.display = "block";
    modalImg.src = imageSrc;
    captionText.innerHTML = "";
}

// Close the modal
function closeModal() {
    var modal = document.getElementById("imageModal");
    modal.style.display = "none";
}

// Close the modal if user clicks anywhere outside of the image
window.onclick = function (event) {
    var modal = document.getElementById("imageModal");
    if (event.target == modal) {
        modal.style.display = "none";
    }
}