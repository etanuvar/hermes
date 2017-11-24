var PagedListSectionApp = {
    init: function () {
        this.bindEvents();
        this.showPreloader = false;
    },
    showPreloader: Boolean,

    bindEvents: function () {
        $('body')
            .on('click', '.sort-link', this.sortList.bind(this))
            .on('click', '.page-size-link', this.changePageSize.bind(this))
            .on('click', "#btn-filter-panel-show", this.filterPanelShow.bind())
            .on('click', '#btn-filter-panel-hide', this.filterPanelHide.bind())
            .on('click', "#table-data tbody tr[details-url] td", this.windowLocationToDetailHref.bind(this))
            .on('click', "#btn-clean-filters", this.cleanFilters.bind(this));
    },

    sortList: function (e) {

        var previousSort = $(e.target).attr('data-previous-sort');
        var sortParam = $(e.target).attr('data-sort-param');
        var newSort = this.getNewSort(previousSort, sortParam);
        $('#sort-param').val(newSort);  // attach sortOption (called function called from pager)
        this.makeAjaxCall();
    },

    reloadPager: function (page) {
        // attach page number (called function called from pager)
        $('#page-number').val(page.toString());
        this.makeAjaxCall();
    },

    changePageSize: function (e) {
        var pageSize = $(e.target).attr('data-val');
        $('#page-number').val("1");
        $('#page-size').val(pageSize.toString());
        this.makeAjaxCall();
    },

    getNewSort: function (previousSort, sortParam) {
        var desc = "_desc";
        var newSort = "";
        if (previousSort.indexOf(sortParam) > -1) {
            if (previousSort.indexOf(desc) > -1) {
                newSort = sortParam;
            }
            else {
                newSort = sortParam + desc;
            }
        }
        else {
            newSort = sortParam;
        }
        return newSort;
    },

    cleanFilters: function () {
        document.getElementById('filter-form').reset();
        if ($('.dropdown-load').length && $('.select-box').length) {
            $('.select-box').hide();
            $('.dropdown-load').show();
            $('.select-box').multiselect('rebuild');
            $('.dropdown-load').hide();
            $('.select-box').show();
        }
        this.makeAjaxCall();
    },

    makeAjaxCall: function () {
        var self = this;
        // MAKE PAGE UNCLICKABLE AND LOAD GIFLOADER HERE
        // Differ ajax-type based on actual query (if filter box is super complicated, it has to be done using POST (to long GET query link)
        var filterForm = $("#filter-form");
        var serializedData = filterForm.serializeArray();

        var actionUrl = filterForm.attr("action");
        var method = "POST";

        //if (actionUrl == "/CooperatorExpress/FilterIndex" || actionUrl == "/ExternalVerification/FilterIndex") {    // ADD CONDITIONS
        //    method = "POST";
        //}

        //var data = JSON.stringify(serializedData);

        this.tableBeginLoad();
        $.ajax({
            type: method,
            url: actionUrl,
            data: serializedData,
            success: function (response) {
                $("#ajax-list-grid").html(response);
                self.tableEndLoad();
                // MAKE PAGE CLICKABLE AGAIN AND UNLOAD GIFLOADER HERE
            }
        });
    },

    tableBeginLoad: function () {
        var self = this;
        $(".mask").addClass("actived");
        $("#page-loader").show();
        $("button").prop("disabled", true);
        $(".btn").prop("disabled", true);
        $("input").prop("disabled", true);
        $("select").prop("disabled", true);
        $("textarea").prop("disabled", true);
        $(".pagination").addClass("pagination-disabled");
    },

    tableEndLoad: function () {
        this.updatePageSize();
        this.reloadPageNumber();
        $(".mask").removeClass("actived");
        $("#page-loader").hide();
        $("#total-item-count-display").html($("#total-item-count").val());
        $("button").prop("disabled", false);
        $(".btn").prop("disabled", false);
        $("input").prop("disabled", false);
        $("select").prop("disabled", false);
        $("textarea").prop("disabled", false);
    },

    tableEndLoadExtended: function () {
        var filterForm = $("#filter-form");
        var actionUrl = $('#IsReport').val();

        if (actionUrl === true) {
            alert("hipi");
        }
        else {
            makeAjaxCall();
        }
    },

    filterPanelShow: function () {
        $("#filter-box").fadeToggle(400);
    },

    filterPanelHide: function () {
        $("#filter-box").fadeOut(400);
    },

    windowLocationToDetailHref: function (e) {

        if ($(e.target).hasClass('unlinked') === false) {
            $("button").prop("disabled", true);
            $(".btn").prop("disabled", true);
            $("input").prop("disabled", true);
            $("select").prop("disabled", true);
            $("textarea").prop("disabled", true);
            $(".pagination").addClass("pagination-disabled")
            window.location = $(e.target).parent().attr("details-url");
        }
    },

    reloadPageNumber: function () {
        var currentActiveAnchorTagForPageNumber = $('#pagination-pagenumbers-container ul.pagination li.active a');
        var number = currentActiveAnchorTagForPageNumber.html();
        $('#page-number').val(number);
    },

    updatePageSize: function () {
        // - 1 (header row)
        var countString = $('#page-size').val();
        var currentActiveAnchorTag = $('#page-size-ul li.active a');
        var valueOfCurrentActiveAnchor = $(currentActiveAnchorTag).attr('data-val');
        if (countString !== valueOfCurrentActiveAnchor) {
            var currentAnchorLiElement = currentActiveAnchorTag.parent();
            currentAnchorLiElement.removeClass('active');
            var newElement = $("#page-size-ul").find("[data-val='" + countString + "']");
            $(newElement).parent().addClass('active');
        }
    }
};

jQuery(function ($) {
    'use strict';
    PagedListSectionApp.init();

    $("#filter-box").delegate('input',
        'keyup',
        function (e) {
            e.preventDefault();
            PagedListSectionApp.makeAjaxCall();
        });
});