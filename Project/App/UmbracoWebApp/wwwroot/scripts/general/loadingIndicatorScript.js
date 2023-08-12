// Function to show the loading indicator
function showLoadingIndicator() {
    const loadingOverlay = document.querySelector('.loading-overlay');
    loadingOverlay.style.display = 'flex';
}

// Function to hide the loading indicator
function hideLoadingIndicator() {
    const loadingOverlay = document.querySelector('.loading-overlay');
    loadingOverlay.style.display = 'none';
}