
async function IncludeHTML(file) {
    const response = await fetch(file);
    return response.text();
}