
/* Script that toggles between two classes. */
function toggleClass(elementId, class1, class2) {
    var element = document.getElementById(elementId);
    if (element.classList.contains(class1)) {
        element.classList.remove(class1);
        element.classList.add(class2);
    } else {
        element.classList.remove(class2);
        element.classList.add(class1);
    }
}