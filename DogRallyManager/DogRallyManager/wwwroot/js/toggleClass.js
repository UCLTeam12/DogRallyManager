
/* Script that toggles between two classes. */
function toggleClass(id, class1, class2) {

    const element = document.getElementById(id);

    // If element contains "class1", remove "class1" and add "class1".
    if (element.classList.contains(class1)) {
        
        element.classList.remove(class1);
        element.classList.add(class2);
    }
    // If element contains "class1", remove "class2" and add "class1".
    else if (element.classList.contains(class2)) {

        element.classList.remove(class2);
        element.classList.add(class1);
    }
}