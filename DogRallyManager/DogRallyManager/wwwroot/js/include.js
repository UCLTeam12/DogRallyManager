
async IncludeCSHTML(file) {
    response = await fetch(file);
    text = await reponse.text()

    return text;
}