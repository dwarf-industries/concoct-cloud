$(document).ready(function() {

    var table = $('#left-right-fix').DataTable( {
        scrollY:        "300px",
        scrollX:        true,
        scrollCollapse: true,
        paging:         false,
        fixedColumns:   {
            leftColumns: 0,
            rightColumns: 0
        }
    } );


    var rfix = $('#right-fix').DataTable({
        scrollY: "300px",
        scrollX: true,
        scrollCollapse: true,
        paging: false,
        fixedColumns: {
            leftColumns: 0,
            rightColumns: 0
        }
    });
    
    var mfix = $('#multi-fix').DataTable( {
        scrollY:        "300px",
        scrollX:        true,
        scrollCollapse: true,
        paging:         false,
        fixedColumns:   {
            leftColumns: 0
        }
    } );
});
