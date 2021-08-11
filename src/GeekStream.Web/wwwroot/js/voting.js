$(document).ready(function() {
    $(".upvote__post").on("click","", function() {
        const id = $(this).attr("articleId");
        const type = $(this).find(".vote").attr("value");

        $.ajax({
            type: "POST",
            url: "/Vote/ProcessVotes",
            dataType: "JSON",
            data: {
                articleId: id,
                type: type
            },
            success: function(data) {
                $("#rating__" + id).html(data);
            }
        });
    }); 

    $(".downvote__post").on("click","", function() {
        const id = $(this).attr("articleId");
        const type = $(this).find(".vote").attr("value");

        $.ajax({
            type: "POST",
            url: "/Vote/ProcessVotes",
            dataType: "JSON",
            data: {
                articleId: id,
                type: type
            },
            success: function(data) {
                $("#rating__" + id).html(data);
            }
        });
    });

    $(".upvote__comment").on("click","", function() {
        const id = $(this).attr("commentId");
        const type = $(this).find(".vote").attr("value");

        $.ajax({
            type: "POST",
            url: "/Vote/ProcessVotesOnReply",
            dataType: "JSON",
            data: {
                commentId : id,
                type: type
            },
            success: function(data) {
                $("#rating__" + id).html(data);
            }
        });
    }); 

    $(".downvote__comment").on("click","", function() {
        const id = $(this).attr("commentId");
        const type = $(this).find(".vote").attr("value");

        $.ajax({
            type: "POST",
            url: "/Vote/ProcessVotesOnReply",
            dataType: "JSON",
            data: {
                commentId : id,
                type: type
            },
            success: function(data) {
                $("#rating__" + id).html(data);
            }
        });
    });

});
