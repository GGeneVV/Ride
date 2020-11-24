$(document).ready(function () {


    $(".os-host").on("scroll", function () {
        console.log($(this).scrollTop());
        if ($(this).scrollTop() > 100) {
            $("#main .top-bar").addClass("sticky-scroll");
        }
        else {
            $("#main .top-bar").removeClass("sticky-scroll");
        }
    });




    $('.search-input input').keyup(function () {
        var value = $(this).val();
        history.pushState(null, '', '/search/' + value);

        $.ajax({
            method: "GET",
            url: window.location.href,
            cache: true,
            beforeSend: function () {
                $(".loading").show();
            },
            success: function (data) {
                var res = $(data).find('#search-result').html()
                console.log(res);
                $('#search-result').html(res);
            },
            complete: function (data) {
                $(".loading").hide();
            },

        });
    })


    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results == null) {
            return null;
        }
        else {
            return results[1] || 0;
        }
    }

});

function sortByTitle() {
    var pageNumber = 1;
    var sortOrder = $.urlParam('sortOrder');
    if (sortOrder == "title") {
        sortOrder = "title_desc";
    }
    else {
        sortOrder = "title";
    }
    var searchString = $.urlParam('searchString');
    var data = { pageNumber, sortOrder, searchString };

    const url = new URL(window.location);
    if (searchString != null) {
        url.searchParams.set('searchString', searchString);
    }
    url.searchParams.set('sortOrder', sortOrder);
    window.history.pushState({}, '', url);

    $.ajax({
        type: "GET",
        data: data,
        url: "https://localhost:5001/Tracks",
        beforeSend: function () {
            $(".loading").show();
        },
        success: function (data) {
            var response = $(data).find('#table-results')[0];
            var pageNumber = $(data).find('#table-results #pageNumber').val();
            $('#table-results #pageNumber').val(pageNumber);
            $('#table-results').html(response);
        },
        complete: function (data) {
            $(".loading").hide();
        },
    });

}


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


function sortByDuration() {
    var pageNumber = 1;
    var sortOrder = $.urlParam('sortOrder');
    if (sortOrder == "duration") {
        sortOrder = "duration_desc";
    }
    else {
        sortOrder = "duration";
    }
    var searchString = $.urlParam('searchString');
    var data = { pageNumber, sortOrder, searchString };

    const url = new URL(window.location);
    if (searchString != null) {
        url.searchParams.set('searchString', searchString);
    }
    url.searchParams.set('sortOrder', sortOrder);
    window.history.pushState({}, '', url);

    $.ajax({
        type: "GET",
        data: data,
        url: "https://localhost:5001/Tracks",
        beforeSend: function () {
            $(".loading").show();
        },
        success: function (data) {
            var response = $(data).find('#table-results')[0];
            var pageNumber = $(data).find('#table-results #pageNumber').val();
            $('#table-results #pageNumber').val(pageNumber);
            $('#table-results').html(response);
        },
        complete: function (data) {
            $(".loading").hide();
        },
    });

}



function sortByArtist() {
    var pageNumber = 1;
    var sortOrder = $.urlParam('sortOrder');
    if (sortOrder == "artist") {
        sortOrder = "artist_desc";
    }
    else {
        sortOrder = "artist";
    }
    var searchString = $.urlParam('searchString');
    var data = { pageNumber, sortOrder, searchString };

    const url = new URL(window.location);
    if (searchString != null) {
        url.searchParams.set('searchString', searchString);
    }
    url.searchParams.set('sortOrder', sortOrder);
    window.history.pushState({}, '', url);
    //window.location.href = url;

    $.ajax({
        type: "GET",
        data: data,
        url: "https://localhost:5001/Tracks",
        beforeSend: function () {
            $(".loading").show();
        },
        success: function (data) {
            var response = $(data).find('#table-results')[0];
            var pageNumber = $(data).find('#table-results #pageNumber').val();
            $('#table-results #pageNumber').val(pageNumber);
            $('#table-results').html(response);
        },
        complete: function (data) {
            $(".loading").hide();
        },
    });

}



