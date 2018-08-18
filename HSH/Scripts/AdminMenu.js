//Javascript toggle for Admin Menu
$(function () {
    $('[data-admin-menu]').hover(function () {
        $('[data-admin-menu]').toggleClass('open');
    });
});