// DataTable to replace the index table for product
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
                    { data: 'customer.name', "width": "15%" }
                ]
        }
    );
}

