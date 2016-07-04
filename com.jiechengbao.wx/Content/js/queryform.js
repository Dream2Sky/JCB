
var InputsCondition;
$(function () {

    $(".QueryForm .ForTips").mouseenter(function (e) {
        $("#Tips").fadeIn("fast").show();
    });
    $(".QueryForm .ForTips").mouseleave(function (e) {
        $("#Tips").fadeOut("fast").hide();
    });
    
    // 判断浏览器是否支持 placeholder
    placeholderLoad();
    function placeholderLoad() {
        if (!placeholderSupport()) {
            $('[placeholder]').focus(function () {
                var input = $(this);
                if (input.val() == input.attr('placeholder')) {
                    input.val('');
                    input.removeClass('placeholder');
                }
            }).blur(function () {
                var input = $(this);
                if (input.val() == '' || input.val() == input.attr('placeholder')) {
                    input.addClass('placeholder');
                    input.val(input.attr('placeholder'));
                }
            }).blur();
        };
    }

    function placeholderSupport() {
        return 'placeholder' in document.createElement('input');
    }

    //根据对应的参数提示要输入的车架和发动机号长度
    var setCity = function (infor) {
        var inforArray = infor.split('*');
        var carPrefix = inforArray[0];
        var carEngineLen = inforArray[1];
        var carCarcodeLen = inforArray[2];
        var carOwnerLen = inforArray[3];
        var cityId = inforArray[4];

        $("#lblViolationCount").text("");
        $("#lblCarNumber").text("");
        $('#txtPrefix').val(carPrefix);
        $('#txtCarNumber').attr('maxLength', 7 - carPrefix.length);
        //                $('#txtCarNumber').val('');
        //                $('#txtCarDrive').val('');
        //                $('#txtCarCode').val('');
        //                $('#txtOwner').val('');
        if (carEngineLen > 0) {
            $('#liCardrive').show();
            if (carEngineLen == 99) {
                $('#txtCarDrive').attr('placeholder', "完整发动机号");
                $('#txtCarDrive').attr('maxLength', 99);
            } else {
                $('#txtCarDrive').attr('placeholder', '发动机号后' + carEngineLen + '位');
                $('#txtCarDrive').attr('maxLength', carEngineLen);
            }
        } else {
            $('#liCardrive').hide();
            $('#txtCarDrive').val('');
        }
        if (carCarcodeLen > 0) {
            $('#liCarFrame').show();
            if (carCarcodeLen == 99) {
                $('#txtCarCode').attr('placeholder', "完整车架号");
                $('#txtCarCode').attr('maxLength', 99);
            } else {
                $('#txtCarCode').attr('placeholder', '车架号后' + carCarcodeLen + '位');
                $('#txtCarCode').attr('carCarcodeLen', 99);
            }
        } else {
            $('#liCarFrame').hide();
            $('#txtCarCode').val('');
        }
        //                if (carOwnerLen > 0) {
        //                    $('#liOwner').show();
        //                    if (carOwnerLen == 99) {
        //                        $('#lblCarOwner').text('完整车辆所有人');
        //                        $('#txtOwner').attr('maxLength', 99);
        //                    } else {
        //                        $('#lblCarOwner').text('车辆所有人后' + carOwnerLen + '位');
        //                        $('#txtOwner').attr('maxLength', carOwnerLen);
        //                    }
        //                } else {
        //                    $('#liOwner').hide();
        //                }
        placeholderLoad();
    };

    loadsCity();
    function loadsCity() {
        // 先清空
        $("#City").empty();
        $("#prefix").empty();
        var index = 0;
        var url;
        var carNumber;
        var carPrefix;
        var carCode;
        var carEngine;
        var province;
        var city;

        $.getJSON("/Web/ashx/GetProvince.ashx", function (data) {

            $("#Province").html(data.province);

            if ($.cookie('carCookie') != null) {
                url = $.url($.cookie('carCookie'));
                carNumber = url.param('carNumber');
                carPrefix = url.param('carPrefix');
                carCode = url.param('carCode');
                carEngine = url.param("carEngine");
                province = url.param("province");
                city = url.param("city");
                $("#Province").val(province);
                $("#Province").trigger("chosen:updated");
            }
            var prefixCity = data.prefixCity;

            $.ajaxSettings.async = false;
            $.getJSON("/Web/ashx/GetCityStr.ashx", function (data1) {
                InputsCondition = data1;
                for (var i = 0; i < InputsCondition.length; i++) {

                    var provinceId = $("#Province").val();

                    if (InputsCondition[i].ProvinceID == provinceId) {

                        for (var j = 0; j < InputsCondition[i].Cities.length; j++) {

                            var citieName = '';
                            if (InputsCondition[i].Cities[j].Name.length > 5) {
                                citieName = InputsCondition[i].Cities[j].Name.substring(0, 5);
                            } else {
                                citieName = InputsCondition[i].Cities[j].Name;
                            }

                            if (prefixCity != "" && prefixCity == InputsCondition[i].Cities[j].CityName) {
                                var option = '<option tag=' + InputsCondition[i].Cities[j].Name + ' title=' + InputsCondition[i].Cities[j].CarNumberPrefix + ' infor=' + InputsCondition[i].Cities[j].CarNumberPrefix + '*' + InputsCondition[i].Cities[j].CarEngineLen + '*' + InputsCondition[i].Cities[j].CarCodeLen + '*' + InputsCondition[i].Cities[j].CarOwnerLen + '*' + InputsCondition[i].Cities[j].CityID + ' proxyEnable=' + InputsCondition[i].Cities[j].ProxyEnable + ' value=' + InputsCondition[i].Cities[j].CityID + ' selected="selected">' + citieName + '</option>';
                                $("#City").append(option);
                                var prefixOption = '<option tag=' + InputsCondition[i].Cities[j].Name + ' title=' + InputsCondition[i].Cities[j].CarNumberPrefix + ' infor=' + InputsCondition[i].Cities[j].CarNumberPrefix + '*' + InputsCondition[i].Cities[j].CarEngineLen + '*' + InputsCondition[i].Cities[j].CarCodeLen + '*' + InputsCondition[i].Cities[j].CarOwnerLen + '*' + InputsCondition[i].Cities[j].CityID + ' proxyEnable=' + InputsCondition[i].Cities[j].ProxyEnable + ' value=' + InputsCondition[i].Cities[j].CarNumberPrefix + ' selected="selected">' + InputsCondition[i].Cities[j].CarNumberPrefix + '</option>';
                                $("#prefix").append(prefixOption);
                                if (index == 0) {
                                    var infor = InputsCondition[i].Cities[j].CarNumberPrefix + '*' + InputsCondition[i].Cities[j].CarEngineLen + '*' + InputsCondition[i].Cities[j].CarCodeLen + '*' + InputsCondition[i].Cities[j].CarOwnerLen + '*' + InputsCondition[i].Cities[j].CityID;
                                    setCity(infor);
                                }
                            } else {
                                var options = '<option tag=' + InputsCondition[i].Cities[j].Name + ' title=' + InputsCondition[i].Cities[j].CarNumberPrefix + ' infor=' + InputsCondition[i].Cities[j].CarNumberPrefix + '*' + InputsCondition[i].Cities[j].CarEngineLen + '*' + InputsCondition[i].Cities[j].CarCodeLen + '*' + InputsCondition[i].Cities[j].CarOwnerLen + '*' + InputsCondition[i].Cities[j].CityID + ' proxyEnable=' + InputsCondition[i].Cities[j].ProxyEnable + ' value=' + InputsCondition[i].Cities[j].CityID + '>' + citieName + '</option>';
                                $("#City").append(options);
                                var prefixOptions = '<option tag=' + InputsCondition[i].Cities[j].Name + ' title=' + InputsCondition[i].Cities[j].CarNumberPrefix + ' infor=' + InputsCondition[i].Cities[j].CarNumberPrefix + '*' + InputsCondition[i].Cities[j].CarEngineLen + '*' + InputsCondition[i].Cities[j].CarCodeLen + '*' + InputsCondition[i].Cities[j].CarOwnerLen + '*' + InputsCondition[i].Cities[j].CityID + ' proxyEnable=' + InputsCondition[i].Cities[j].ProxyEnable + ' value=' + InputsCondition[i].Cities[j].CarNumberPrefix + '>' + InputsCondition[i].Cities[j].CarNumberPrefix + '</option>';
                                $("#prefix").append(prefixOptions);
                                if (index == 0) {
                                    var infors = InputsCondition[i].Cities[j].CarNumberPrefix + '*' + InputsCondition[i].Cities[j].CarEngineLen + '*' + InputsCondition[i].Cities[j].CarCodeLen + '*' + InputsCondition[i].Cities[j].CarOwnerLen + '*' + InputsCondition[i].Cities[j].CityID;
                                    setCity(infors);
                                }
                            }

                            index++;

                        }
                    }
                }
            });
            $.ajaxSettings.async = true;

            if ($.cookie('carCookie') != null) {
                $("#City").val(city);
                $("#prefix").val(carPrefix);
                if (carPrefix != null) {
                    $('#txtCarNumber').val(carNumber.replace(carPrefix, '').toUpperCase());
                }

                $('#txtCarCode').val(carCode);
                $('#txtCarDrive').val(carEngine);
            }
            $("#City").trigger("chosen:updated");
            $("#prefix").trigger("chosen:updated");

        });

        //根据省份选择城市初始化对应的车牌，车架，发动机长度信息
        $("#Province").change(function () {

            // 先清空
            $("#City").empty();
            $("#prefix").empty();
            var index = 0;
            for (var i = 0; i < InputsCondition.length; i++) {

                var provinceId = $("#Province").val();
                if (InputsCondition[i].ProvinceID == provinceId) {

                    for (var j = 0; j < InputsCondition[i].Cities.length; j++) {

                        var citieName = '';
                        if (InputsCondition[i].Cities[j].Name.length > 5) {
                            citieName = InputsCondition[i].Cities[j].Name.substring(0, 5);
                        } else {
                            citieName = InputsCondition[i].Cities[j].Name;
                        }

                        var option = '<option  tag=' + InputsCondition[i].Cities[j].Name + ' title=' + InputsCondition[i].Cities[j].CarNumberPrefix + ' infor=' + InputsCondition[i].Cities[j].CarNumberPrefix + '*' + InputsCondition[i].Cities[j].CarEngineLen + '*' + InputsCondition[i].Cities[j].CarCodeLen + '*' + InputsCondition[i].Cities[j].CarOwnerLen + '*' + InputsCondition[i].Cities[j].CityID + ' proxyEnable=' + InputsCondition[i].Cities[j].ProxyEnable + ' value=' + InputsCondition[i].Cities[j].CityID + '>' + citieName + '</option>';
                        $("#City").append(option);
                        var prefixOption = '<option  tag=' + InputsCondition[i].Cities[j].Name + ' title=' + InputsCondition[i].Cities[j].CarNumberPrefix + ' infor=' + InputsCondition[i].Cities[j].CarNumberPrefix + '*' + InputsCondition[i].Cities[j].CarEngineLen + '*' + InputsCondition[i].Cities[j].CarCodeLen + '*' + InputsCondition[i].Cities[j].CarOwnerLen + '*' + InputsCondition[i].Cities[j].CityID + ' proxyEnable=' + InputsCondition[i].Cities[j].ProxyEnable + ' value=' + InputsCondition[i].Cities[j].CarNumberPrefix + '>' + InputsCondition[i].Cities[j].CarNumberPrefix + '</option>';
                        $("#prefix").append(prefixOption);
                        if (index == 0) {
                            var infor = InputsCondition[i].Cities[j].CarNumberPrefix + '*' + InputsCondition[i].Cities[j].CarEngineLen + '*' + InputsCondition[i].Cities[j].CarCodeLen + '*' + InputsCondition[i].Cities[j].CarOwnerLen + '*' + InputsCondition[i].Cities[j].CityID;
                            setCity(infor);
                        }
                        index++;

                    }
                }
            }

            
        });

        //根据城市 初始化对应的车牌，车架，发动机长度信息
        $("#City").change(function () {

            var infor = $('#City option[value=' + $(this).val() + ']').attr('infor'); //$(this).$("option").attr('infor');//$(this).attr('value');
            var inforArray = infor.split('*');
            var carPrefix = inforArray[0];

            $("#prefix").val(carPrefix);
            
            setCity(infor);
        });
        //根据车牌 初始化对应的车架，发动机长度信息
        $("#prefix").change(function () {

            var infor = $('#prefix option[value=' + $(this).val() + ']').attr('infor'); //$(this).$("option").attr('infor');//$(this).attr('value');
            var inforArray = infor.split('*');
            var city = inforArray[4];
            $("#City").val(city);
            
            setCity(infor);
        });

        
    };
})


function onSubmit() {

    if (checkInputsConditon()) {
        addCookie();
        $.ajaxSetup({
            async: false
        });
        
    }
    //else {
    //    $.Zebra_Dialog('您好！您输入的信息不符合要求，请按提示信息重新输入！',
    //                        {
    //                            'type': 'error',
    //                            'title': '错误信息提示框'
    //                        });

    //    return false;
    //}
};

var checkInputsConditon = function () {
    var MinLen = 5;
    var carPrefix = $('#prefix').val().toUpperCase();
    var carNumber = carPrefix + $('#txtCarNumber').val().toUpperCase();
    var carCode = '';
    var carEngine = '';
    var carOwner = '';
    var $carCode = $('#txtCarCode');
    var $carEngine = $('#txtCarDrive');
    //var $carOwner = $('#txtOwner');

    var vCode = $("#Code").val();//用户填写的验证码

    if (vCode.length < 4) {
        $.Zebra_Dialog('您好！请输入验证码！',
                       {
                           'type': 'error',
                           'title': '错误信息提示框'
                       });
        return false;
    }

    if ($carCode.length > 0 && $('#txtCarCode').attr('placeholder') != $carCode.val())
        carCode = $carCode.val().replace(/undefined/g, '');
    if ($carEngine.length > 0 && $('#txtCarDrive').attr('placeholder') != $carEngine.val())
        carEngine = $carEngine.val().replace(/undefined/g, '');
    //                if ($carOwner.length > 0)
    //                    carOwner = $carOwner.val().replace(/undefined/g, '');

    var conditions = $('option[infor ^=' + carPrefix + ']').attr('infor');
    if (conditions.length > 0) {
        var inforArray = conditions.split('*');
        var carEngineLen = inforArray[1] == 99 ? MinLen : inforArray[1];
        var carCarcodeLen = inforArray[2] == 99 ? MinLen : inforArray[2];
        //var carOwnerLen = inforArray[3] == 99 ? 2 : inforArray[3];                                //|| carOwnerLen > carOwner.length
        if (carNumber.length < 7 || carEngineLen > carEngine.length || carCarcodeLen > carCode.length) {
            $.Zebra_Dialog('您好！您输入的信息不符合要求，请按提示信息重新输入！',
                       {
                           'type': 'error',
                           'title': '错误信息提示框'
                       });
            return false;
        } else {
            return true;
        }
    }
    return false;
};

var addCookie = function () {
    var carPrefix = $('#prefix').val().toUpperCase();
    var carNumber = carPrefix + $('#txtCarNumber').val().toUpperCase();
    var carCode = $('#txtCarCode').val().replace(/undefined/g, '');
    var carEngine = $('#txtCarDrive').val().replace(/undefined/g, '');
    //  var carOwner = $('#txtOwner').val();
    var phone = ''; //$('#Phone').val().replace(/undefined/g, '');
    var code = ''; //$('#Code').val().replace(/undefined/g, '');   
    var cookie = '';
    if ($.cookie('carCookie') != null) {
        var url = $.url($.cookie('carCookie'));
        if (url.param('carNumber') == carNumber) {
            if (url.param('carCode') != null && url.param('carCode').length > carCode) {
                carCode = url.param('carCode');
            }
            if (url.param('carEngine') != null && url.param('carEngine').length > carEngine) {
                carEngine = url.param('carEngine');
            }
            
        }
    }
    //phone = phone.replace(/undefined/g, '');
    var province = $('#Province').val();
    var city = $('#City').val();
    var cookieInfor = "?carPrefix=" + carPrefix + "&carNumber=" + carNumber + "&carCode=" + carCode + "&carEngine=" + carEngine + "&phone=" + phone + "&province=" + province + "&city=" + city + "&code=" + code;
    
    // 方便读取参数,直接采用网站链接的形式 无其它特殊意义
    cookie = "http://www.cx580.com" + cookieInfor;
    //            cookie = cookie.replace(/undefined/g, '');
    $.cookie('carCookie', cookie, { expires: 1, path: '/' });
};        