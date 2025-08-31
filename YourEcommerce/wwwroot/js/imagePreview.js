function triggerFileInput(inputId) {
    document.getElementById(inputId).click();
}

function setupImagePreview(inputSelector, imgSelector) {
    const input = document.querySelector(inputSelector);
    const img = document.querySelector(imgSelector);

    if (!input || !img) return;

    input.addEventListener('change', (event) => {
        const file = event.target.files[0];
        if(file) {
            const reader = new FileReader();
            reader.onload = e => img.src = e.target.result;
            reader.readAsDataURL(file);
        }
    });
}