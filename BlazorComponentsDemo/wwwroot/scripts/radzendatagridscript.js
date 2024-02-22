// By default, RadzenDataGrid will initially display '10' for the page size dropdown label on page load.
// This function sets the label to the correct minimum page size number.
export function changeDropdownLabelText(minPageSize) {
    // Get the label element with both classes "rz-dropdown-label" and "rz-inputtext"
    var label = document.querySelector('.rz-dropdown-label.rz-inputtext');

    if (label) {
        label.textContent = minPageSize;
    } else {
        return;
    }
}

export function showAlert(message) {
    alert(message);
}
