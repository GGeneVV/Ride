$(".os-host").on("scroll", function () {
    console.log($(this).scrollTop());
    if ($(this).scrollTop() > 100) {
        $("#main .top-bar").addClass("sticky-scroll");
    }
    else {
        $("#main .top-bar").removeClass("sticky-scroll");
    }
});

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        // x.innerHTML = "Geolocation is not supported by this browser.";
    }
}

function showPosition(position) {
    $('#from').val(position.coords.latitude + ',' + position.coords.longitude);
}


$('.search-input input').keyup(function () {
    var value = $(this).val();
    history.pushState(null, '', '/search/' + value);

    $.ajax({
        method: "GET",
        url: window.location.href,
        cache: true,
        //data: { name: "John", location: "Boston" }
    }).done(function (data) {
        var res = $(data).find('#search-result').html()
        console.log(res);
        $('#search-result').html(res);
    });
})
