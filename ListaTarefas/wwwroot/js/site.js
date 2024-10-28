$(document).ready(function () {
    $('#Tarefas').DataTable();

});
setTimeout(function () {
    $(".alert").fadeOut("slow", function () {
        $(this).alert('close');
    });
}, 5000);
