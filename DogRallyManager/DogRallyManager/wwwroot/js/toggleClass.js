
/* A function that takes an id and two classes as parameters
   and toggles between the two classes in the element with
   the given id when called. */
function toggleClass(id, class1, class2) {

    const element = document.getElementById(id);

    if (element.classList.contains(class1)) {

        element.classList.remove(class1);
        element.classList.add(class2);
    }
}