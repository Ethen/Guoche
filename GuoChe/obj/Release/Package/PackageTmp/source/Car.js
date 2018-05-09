﻿var elemnets = {
    ids: ["CarName", "ModelCode", "ContractCode", "SupplierID"],
    methods: [
        {
            required: true,
            messages: ["请输入车辆名称", "", "", ""]
        },
        {
            required: true,
            messages: ["请选择车型代码", "", "", ""]
        },
        {
            required: true,
            messages: ["请输入合同编号", "", "", ""]
        },
        {
            required: true,
            messages: ["请选择经销商", "", "", ""]
        }
    ]
};


var FileInput = function () {
    var oFile = new Object();

    //初始化fileinput控件（第一次初始化）
    oFile.Init = function (ctrlName, uploadUrl) {
        var control = $('#' + ctrlName);

        //初始化上传控件的样式
        control.fileinput({
            language: 'zh', //设置语言
            uploadUrl: uploadUrl, //上传的地址
            allowedFileExtensions: ['jpg', 'jpeg'],//接收的文件后缀
            showUpload: true, //是否显示上传按钮
            showCaption: false,//是否显示标题
            browseClass: "btn btn-primary", //按钮样式
            dropZoneEnabled: false,//是否显示拖拽区域
            maxFileSize: 20000,//单位为kb，如果为0表示不限制文件大小
            minFileCount: 0,//限制上传的文件数量
            maxFileCount: 5,
            enctype: 'multipart/form-data',
            validateInitialCount: true,
            previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
            msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！",
            layoutTemplates: {
                actionUpload: "",
                actionDelete:""
            }
        });

        //导入文件上传完成之后的事件
        $("#file").on("fileuploaded", function (event, data, previewId, index) {
            if (!!data) {
                var aid = data.response[0].AttachmentID;
                var aids = $("#AttachmentIDs").val(), fpath = data.response[0].FilePath.replace("~", ""), fName = data.response[0].FileName + data.response[0].FileExtendName;
                if (aids.length > 0) {
                    aids = aids + "," + aid;
                }
                else {
                    aids = aid;
                }
                $("#AttachmentIDs").val(aids);
                $("#picContainer").append('<span class="picRemove"  val="' + aid + '" style="height:0px"></span>');
                $("#picContainer").append('<img src="' + fpath + '" alt="' + fName + '" style="width: 150px; height: 150px; margin-top: 15px">');
                carInfo.clearUpload();
                carInfo.initPicInfo();
            }
            
        }).on('fileerror', function (event, data, msg) {
            alert("上传失败，失败原因" + msg);
        });

    }
    return oFile;
};

var carInfo = {
    init: function () {
        valid.init(elemnets);
        carInfo.regEvent();
        carInfo.initPicInfo();
        $("#ModelCode").trigger("change");
    },

    regEvent: function () {

        $("#ModelCode").change(function () {
            if ($(this).val() == "C02" || $(this).text() == "货车") {
                $("#trackContainer").show();
            }
            else {
                $("#trackContainer").hide();
            }

        })

        $("#save").click(function () {
            if (valid.validate()) {
                $("#carForm").submit();
            }
        });


    },

    clearUpload: function () {
        $(".close").trigger("click");
        $(".fileinput-cancel-button").attr("disabled", "disabled").hide();
        $(".btn-primary").removeAttr("disabled");
        $("#file").removeAttr("disabled");
        var ids=$("#AttachmentIDs").val().split(",");

        if (ids.length > (maxPicCount-1))
        {
            $("#uploadC").hide();
        }
    },

    initPicInfo: function () {
        $("#picContainer>img").on("mouseenter", function () {
            $(this).prev(".picRemove").addClass("icon-trash").css("height", "15px");
        }).on("mouseout", function () {
            $(this).prev(".picRemove").removeClass("icon-trash").css("height", "0px");
        });
        $(".picRemove")
          .on("click", function () {
              var aids = $("#AttachmentIDs").val(), aid = $(this).attr("val");
              if (aids.indexOf(aid + ",") > -1) {
                  aids = aids.replace(aid + ",", "");
              }
              else {
                  aids = aids.replace(aid, "");
              }
              $(this).next("img").remove();
              $(this).remove();
              $("#AttachmentIDs").val(aids);

              $("#uploadC").show();
          }).on("mouseenter", function () {
              if ($(this).hasClass("icon-trash")) {
                  $(this).css("height", "15px");
              }
              else {
                  $(this).addClass("icon-trash").css("height", "15px");
              }
          });
    }
}