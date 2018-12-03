var button = document.getElementById("Like")

button.onclick = function () {
    if (content.ClassName == "Open") {
        // Update Database
        //code required
    }
    else {
        button.innerHTML = "Liked"
        
    }
};

$('#btnClickme').on('click mouseover mouseout', function (event) {
    if (event.type == 'click') {
        $('#divResult').html('Button Clicked at X = '
            + event.pageX + 'Y =' + event.pageY);
    }
    else if (event.type == 'mouseover') {
        $(this).addClass('ButtonStyle');
    }
    esle{
        $(this).removeClass('ButtonStyle');
    }
});