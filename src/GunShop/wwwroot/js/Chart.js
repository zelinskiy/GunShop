var table = $('#allCommoditiesTable').DataTable({
    searching: false,
    paging: false,
    info: false,
    columns: [
        { orderable: true },
        { orderable: false }
    ]
});