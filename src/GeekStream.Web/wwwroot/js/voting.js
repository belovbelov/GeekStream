$(document).ready(function() {
    $(".vote__post").on("click","", function() {
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

    $(".vote__comment").on("click","", function() {
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

        $('img[data-enlargeable]').addClass('img-enlargeable').click(function() {
            var src = $(this).attr('src');
            var modal;

            function removeModal() {
                modal.remove();
                $('body').off('keyup.modal-close');
            }
            modal = $('<div>').css({
                background: 'RGBA(0,0,0,.5) url(' + src + ') no-repeat center',
                backgroundSize: 'contain',
                width: '100%',
                height: '100%',
                position: 'fixed',
                zIndex: '10000',
                top: '0',
                left: '0',
                cursor: 'zoom-out'
            }).click(function() {
                removeModal();
            }).appendTo('#images');
            $('#images').on('keyup.modal-close', function(e) {
                if (e.key === 'Escape') {
                    removeModal();
                }
            });
        });
});
