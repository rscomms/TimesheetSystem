
//Validates form fields
function validateForm() {
    var hoursWorked = document.getElementById("hoursWorked").value;
    var errorHours = document.getElementById("errorHours");

    if (hoursWorked == "") {
        document.getElementById("hoursWorked").value = 8;
        return true;
    }

    if (hoursWorked <= 0) {
        errorHours.style.display = "inline";
        return false;
    } else {
        errorHours.style.display = "none";
        return true;
    }
}

//Downloads timesheet table as a CSV file
function downloadCSV() {
    var table = document.getElementById("timesheetTable");
    var rows = table.rows;
    var csv = [];

    // Iterates through the rows to extract the data
    for (var i = 0; i < rows.length; i++) {
        var cells = rows[i].cells;
        var row = [];
        for (var j = 0; j < cells.length; j++) {
            row.push('"' + cells[j].innerText.replace(/"/g, '""') + '"');
        }
        csv.push(row.join(','));
    }

    // Creates a link element for downloading
    var csvFile = new Blob([csv.join('\n')], { type: 'text/csv' });
    var link = document.createElement("a");
    link.href = URL.createObjectURL(csvFile);
    link.download = "timesheet_entries.csv";
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
