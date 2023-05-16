var dataTable;

$(document).ready(function () {
    dataTable();
});

function dataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "Admin/Course/GetAll"
        },
        "columns": [
            { "data": "Course Name", "width": "15%" }
            { "data": "Semester", "width": "15%" }
            { "data": "Description", "width": "15%" }
            { "data": "Course Image", "width": "15%" }
            { "data": "Update Date", "width": "15%" }
        ]
    });
}

function dataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "Admin/Category/GetAll"
        },
        "columns": [
            { "data": "Category Name", "width": "15%" }
            { "data": "Description", "width": "15%" }
            { "data": "Update Date", "width": "15%" }
            { "data": "Image Url", "width": "15%" }
        ]
    });
}