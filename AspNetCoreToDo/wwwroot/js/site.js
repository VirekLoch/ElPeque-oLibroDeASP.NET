// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    $('.done-chekbox').click(function (e) {
        markChecked(e.target);
    });
});

function markChecked(checkbox) {
    checkbox.disabled = true;

    var row = checkbox.closest('tr');
    $(row).addClass('done');
    
    var form = checkbox.closest('form');
    form.submit();
}
