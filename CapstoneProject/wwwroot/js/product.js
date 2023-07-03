﻿// DataTable to replace the index table for product
// Use the admin/product/getall endpoint to get the JSON for the data column names
// Must be exact, case sensitive
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable()
{
    dataTable = $('#myTable').DataTable(
        {
            "ajax":
            {
                url: '/admin/product/getall'
            },
            "columns":
                [
                    { data: 'name', "width": "15%" },
                    { data: 'productCode', "width": "15%" },
                    { data: 'netWeight', "width": "5%" },
                    { data: 'grossWeight', "width": "5%" },
                    { data: 'isHazardous', "width": "5%" },
                    { data: 'uNnumber', "width": "5%" },
                    { data: 'price', "width": "5%" },
                    { data: 'bulkRate10', "width": "5%" },
                    { data: 'bulkRate100', "width": "5%" },
                    { data: 'inventory', "width": "5%" },
                    { data: 'customer.name', "width": "5%" },
                    {
                        data: 'id',
                        "render": function (data) {
                            return `<div class="w-75 btn-group" role="group">
                            <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>  
                            <a href="/admin/product/delete/${data}"class="btn btn-danger mx-2"><i class="bi bi-dash-circle"></i> Delete</a>
                            </div>`
                        },
                        "width" : "25%"
                    }
                ]
        }
    );
}
