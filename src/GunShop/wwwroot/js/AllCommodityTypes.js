var table = $('#allCommoditiesTable').DataTable({
    searching: false,
    paging: false,
    info:false,
    columns: [
        { orderable: true },
        { orderable: true },
        { orderable: false },
        { orderable: false },
        { orderable: false },
        { orderable: true },
        { orderable: false },
        { orderable: false }
    ]
});