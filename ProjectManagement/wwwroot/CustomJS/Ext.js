function printDiv(divId) {
    var divToPrint = document.getElementById(divId);
    var newWindow = window.open('', '', 'height=600,width=800');
    newWindow.document.write('<html><head><title>Print</title>');
    newWindow.document.write('</head><body >');
    newWindow.document.write(divToPrint.outerHTML);
    newWindow.document.write('</body></html>');
    newWindow.document.close();
    newWindow.print();
}
function downloadFile(fileName, fileContentBase64) {
    var link = document.createElement('a');
    link.href = 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,' + fileContentBase64;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
function downloadFileGeneric(fileName, fileContentBase64, mimeType) {
    var link = document.createElement('a');
    link.href = 'data:' + (mimeType || 'application/octet-stream') + ';base64,' + fileContentBase64;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

// Site theme: switched by setting data-theme on <html>, which drives the CSS
// variables in wwwroot/css/app.css. Persisted in localStorage so it survives
// reloads; set once per full page load from MainLayout (in-app navigation
// doesn't reload <html>, so it doesn't need re-applying on every page).
function setAppTheme(theme) {
    document.documentElement.setAttribute('data-theme', theme);
    try { localStorage.setItem('pm-theme', theme); } catch (e) { /* private window etc. */ }
}
function getAppTheme() {
    try { return localStorage.getItem('pm-theme') || 'professional'; } catch (e) { return 'professional'; }
}