var Xishu_B_Shape;
var Xishu_X_Shape;
var sub_marid;
var tsaid;



/*材料计划JS计算：*/
function AutoTuHao(obj) {
    if (document.getElementById("ckbTuhao").checked) {
        if (obj.value != "") {
            var table = document.getElementById(getClientId().Id1);
            var tablerows = table.rows.length;
            var tr = table.getElementsByTagName("tr");
            var index = obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var t = parseInt(index) + 1;
            if (t < tablerows) {
                if (table.rows[t].cells[2].getElementsByTagName("input")[0].value == "") {
                    table.rows[t].cells[2].getElementsByTagName("input")[0].value = obj.value;
                }
            }
        }
    }
}

//长宽改变
function auto(input) {
    var table = document.getElementById(getClientId().Id1);
    var tr = table.getElementsByTagName("tr");
    var index = input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    var cailiaocd = tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //长度
    var cailiaokd = tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //宽度
    var shuliang = tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[1].value.replace(/(\s*$)/g, ""); //数量
    var p_shuliang = tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[2].value.replace(/(\s*$)/g, ""); //数量
    var dw = tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value; //技术单位
    var caigoudw = tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value; //采购单位
    var mapishape = tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value;
    var fix = tr[index].getElementsByTagName("td")[28].getElementsByTagName("select")[0].value;
    GetXiShu_TMOrg(fix);
    var pattem = /^\d+(\.\d+)?$/; //数量验证
    // var pattem2 = /^[0-9]*$/; //长、宽验证
    var pattem2 = /^\d+(\.\d+)?$/;
    if (cailiaocd == "") {
        cailiaocd = "0";
        tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value = "";
    }

    if (cailiaokd == "") {
        cailiaokd = "0";
        tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value = "";
    }

    if (shuliang == "") {
        shuliang = "1";
        tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value = "";
    }

    //材料长度格式检查
    if (pattem2.test(cailiaocd)) {
        if (parseInt(cailiaocd) > 10000) {
            alert('提示:输入【材料长度】超出10米,请核实！！!');
        }
    }
    else {
        alert('提示:输入【材料长度】格式不正确!');
        tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value = "";
        return false;
    }
    //材料宽度检查
    if (pattem2.test(cailiaokd)) {
        if (parseInt(cailiaokd) > 10000) {
            alert('提示:输入【材料宽度】超出10米,请核实！！!');
        }
    }
    else {
        alert('提示:输入【材料宽度】格式不正确!');
        tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value = "";
        return false;
    }

    //数量检查
    if (pattem.test(shuliang)) {
        if (parseInt(shuliang) > 50) {
            if (input.parentNode.cellIndex == 9) {
                alert('提示:输入【数量】超出50,请核实！！!');
            }
        }
    }
    else {
        alert('提示:输入【数量】格式不正确!');
        tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value = "1";
        return false;
    }
    //格式检查完毕，开始处理数据
    var marid = tr[index].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    var cailiaoname = tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    var dzh = tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //图纸单重
    if (marid != "") {
        var cailiaodzh = "0";
        var cailiaozongzhong = "0";
        var bgzmy = "0";
        var cailiaoguige = tr[index].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
       if(cailiaoguige.indexOf("+")>-1){
       cailiaoguige=parseFloat(cailiaoguige.split('+')[0])+parseFloat(cailiaoguige.split('+')[1]);
       }
      
        var mapishape = tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
        var lilunzhl = tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
        var zongzhong = "0";
        var cailiaozongchang = tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
        var sub_marid = marid.substring(0, 5);
        var child_sub_marid = sub_marid.substring(0, 2);

        if (sub_marid != "01.01") {
            if (mapishape == "板" || cailiaoname.indexOf("钢板") > -1 || cailiaoname.indexOf("栅格板") > -1 || cailiaoname.indexOf("花纹板") > -1 || cailiaoname.indexOf("钢格板") > -1 || cailiaoname.indexOf("钢板网") > -1) {
                if (cailiaocd != 0 && cailiaokd != 0) {
                    if (cailiaoname.indexOf("花纹板") > -1 || cailiaoname.indexOf("钢板网") > -1 || cailiaoname.indexOf("钢格板") > -1 || cailiaoname.indexOf("栅格板") > -1 || cailiaoname.indexOf("电焊网") > -1) {
                        cailiaodzh = (cailiaocd * cailiaokd * lilunzhl / 1000000).toFixed(3);
                        if (cailiaoname.indexOf("钢格板") > -1) {
                            Xishu_B_Shape = "1";
                        }

                    }
                    else {
                        cailiaodzh = (cailiaoguige * cailiaocd * cailiaokd * lilunzhl / 1000000).toFixed(3);
                    }


                    //计算实际单重和总重
                    bgzmy = (cailiaocd * cailiaokd / 1000000).toFixed(3); //材料形状规则



                }

                cailiaozongzhong = (cailiaodzh * p_shuliang * parseFloat(Xishu_B_Shape)).toFixed(3);
                dzh = cailiaodzh; //实际单重不根据材料单重计算
                zongzhong = (dzh * shuliang).toFixed(3);
            }
            else if (mapishape == "型" || mapishape == "圆")//cailiaoname.indexOf("圆钢")>-1||cailiaoname.indexOf("型钢")>-1||cailiaoname=="扁钢"||cailiaoname.indexOf("焊管")>-1||cailiaoname.indexOf("焊接管")>-1||cailiaoname=="无缝管"||cailiaoname=="无缝钢管"||cailiaoname.indexOf("槽钢")>-1||cailiaoname.indexOf("角钢")>-1||cailiaoname=="工字钢"||cailiaoname=="方钢"||cailiaoname=="方钢管"||cailiaoname=="矩形管"||cailiaoname.indexOf("轨道")>-1
            {
                if (cailiaocd != 0) {

                    cailiaodzh = (cailiaocd * lilunzhl / 1000).toFixed(3);
                    cailiaozongzhong = (cailiaodzh * p_shuliang * parseFloat(Xishu_X_Shape)).toFixed(3);
                    cailiaozongchang = (cailiaocd * p_shuliang * parseFloat(Xishu_X_Shape)).toFixed(3);
                    dzh = cailiaodzh;
                    zongzhong = (dzh * shuliang).toFixed(3);
                }

            }
            //采购单位为平米时计算面域
            else if (dw.indexOf("平米") > -1 || dw.indexOf("平方米") > -1 || dw.indexOf("m2") > -1 || dw.indexOf("M2") > -1 || dw.indexOf("㎡") > -1) {
            bgzmy = (cailiaocd * cailiaokd / 1000000).toFixed(3);
            }
            else if (caigoudw == "米" || caigoudw == "m" || caigoudw == "M" || caigoudw == "米" || caigoudw == "m" > -1 || caigoudw == "M") {
                if (cailiaocd != "") {

                    cailiaozongzhong = (cailiaodzh * p_shuliang * parseFloat(Xishu_X_Shape)).toFixed(3);
                    cailiaozongchang = (cailiaocd * p_shuliang * parseFloat(Xishu_X_Shape)).toFixed(3);
                    dzh = cailiaodzh;
                    zongzhong = (dzh * shuliang).toFixed(3);
                    cailiaodzh = (tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value * cailiaocd / 1000).toFixed(3); //材料单重
                }
            }


        }
        else {
            if (caigoudw == "米" || caigoudw == "m" || caigoudw == "M" || caigoudw == "米" || caigoudw == "m" || caigoudw == "M") {
                tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value = (cailiaocd * p_shuliang).toFixed(3);

                tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value = (tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value * cailiaocd / 1000).toFixed(3);  //图纸单重
                tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value = (tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value * cailiaocd / 1000).toFixed(3); //材料单重
                tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = (tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value * p_shuliang * cailiaocd / 1000).toFixed(3);
                tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = (tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value * p_shuliang * cailiaocd / 1000).toFixed(3);

            }
            else {

                tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value = (tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value).toFixed(3); //图纸单重
                tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value = (tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value).toFixed(3); //材料单重
                tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = (tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value * shuliang).toFixed(3);
                tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = (tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value * shuliang).toFixed(3);

            }
        }

//        tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value = dzh;
        tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value = cailiaodzh;
        tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = cailiaozongzhong;
        tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value = bgzmy;
        tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[1].value = bgzmy * parseFloat(Xishu_B_Shape) * p_shuliang;
//        tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = zongzhong;
        tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value = cailiaozongchang;



        //根据单位，赋值用量
        if (dw == "m2" || dw == "平米") {
            tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = (bgzmy * parseFloat(Xishu_B_Shape) * p_shuliang).toFixed(3);

        }
        else if (dw == "m" || dw == "米") {
            tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = (parseFloat(cailiaozongchang) / 1000).toFixed(3);
        }
        else {
            tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = p_shuliang;
        }

    }
    else {
        if (input.parentNode.cellIndex != 9)//物料编码为空时，输入长宽无效
        {
            if (parseInt(cailiaocd) > 0 || parseInt(cailiaokd) > 0) {
                tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value = "0";
                tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value = "0";
                alert("提示:物料编码为空，输入【材料长度】【材料宽度】无效！！！");
            }
        }
    }
}






//数量改变
function autoshuliang(input) {
    var table = document.getElementById(getClientId().Id1);

    var number = 1;
    if (document.getElementById(getClientId().Id4) != null) {
        number = document.getElementById(getClientId().Id4).value;
    }

    var tr = table.getElementsByTagName("tr");
    var index = input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    var cailiaocd = tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //长度
    var cailiaokd = tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //宽度
    var shuliang = tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //数量
    shuliang = shuliang * number;
    tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[1].value = shuliang;
    tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[2].value = shuliang;
    var mapishape = tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value;
    var dw = tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value; //技术单位-采购单位
    var caigoudw = tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value;
    var fix = tr[index].getElementsByTagName("td")[28].getElementsByTagName("select")[0].value;
    GetXiShu_TMOrg(fix);

    var pattem = /^\d+(\.\d+)?$/; //数量验证
    //   var pattem2 = /^[0-9]*$/; //长、宽验证
    var pattem2 = /^\d+(\.\d+)?$/;
    if (cailiaocd == "") {
        cailiaocd = "0";
        tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value = "";
    }

    if (cailiaokd == "") {
        cailiaokd = "0";
        tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value = "";
    }

    if (shuliang == "") {
        shuliang = "1";
        tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value = "";
    }

    //材料长度格式检查
    if (pattem2.test(cailiaocd)) {
        if (parseInt(cailiaocd) > 100000) {
            alert('提示:输入【材料长度】超出100米,请核实！！!');
        }
    }
    else {
        alert('提示:输入【材料长度】格式不正确!');
        tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value = "";
        return false;
    }
    //材料宽度检查
    if (pattem2.test(cailiaokd)) {
        if (parseInt(cailiaokd) > 100000) {
            alert('提示:输入【材料宽度】超出100米,请核实！！!');
        }
    }
    else {
        alert('提示:输入【材料宽度】格式不正确!');
        tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value = "";
        return false;
    }
    //数量检查
    if (pattem.test(shuliang)) {
        if (parseInt(shuliang) > 50) {
            if (input.parentNode.cellIndex == 9) {
                alert('提示:输入【单台数量】超出50,请核实！！!');
            }
        }
    }
    else {
        alert('提示:输入【单台数量】格式不正确!');
        tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value = "";
        tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[1].value = "";
        tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[2].value = "";
        return false;
    }
    //格式检查完毕，开始处理数据
    var marid = tr[index].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    var cailiaoname = tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    var dzh = tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //图纸单重
    if (marid != "") {
        var cailiaodzh = tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
        var cailiaozongzhong = tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
        var bgzmy = tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
        var p_mianyu = tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[1].value.replace(/(\s*$)/g, "");
        var cailiaoguige = tr[index].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
        
         if(cailiaoguige.indexOf("+")>-1){
       cailiaoguige=parseFloat(cailiaoguige.split('+')[0])+parseFloat(cailiaoguige.split('+')[1]);
       }
        var lilunzhl = tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
        var zongzhong = tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
        var cailiaozongchang = tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
        var sub_marid = marid.substring(0, 5);
        var child_sub_marid = sub_marid.substring(0, 2);
        if (child_sub_marid != "02") //去掉非低值易耗品限制
        {
            if (marid.indexOf("01.01") > -1)//理论重量*数量
            {
                if (caigoudw.indexOf("米") > -1 || caigoudw.indexOf("m") > -1 || caigoudw.indexOf("M") > -1 || caigoudw.indexOf("米") > -1 || caigoudw.indexOf("m") > -1 || caigoudw.indexOf("M") > -1) {
                    cailiaozongchang = (cailiaocd * shuliang).toFixed(3);
                  //  tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value = (lilunzhl * cailiaocd / 1000).toFixed(3);
                    tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value = (lilunzhl * cailiaocd / 1000).toFixed(3);
                    cailiaozongzhong = (lilunzhl * shuliang * cailiaocd / 1000).toFixed(3);
                    zongzhong = (lilunzhl * shuliang * cailiaocd / 1000).toFixed(3);

                }
                else {

                 //   tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value = tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value;
                    tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value = tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value;
                    cailiaozongzhong = (tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value * shuliang).toFixed(3);
                    zongzhong = (tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value * shuliang).toFixed(3);
                }
                //  tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = zongzhong;
              
                tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = cailiaozongzhong;
            }
            else if (mapishape == "板" || cailiaoname.indexOf("花纹板") > -1 || cailiaoname.indexOf("钢板网") > -1 || cailiaoname.indexOf("钢格板") > -1) {
                if (cailiaocd != 0 && cailiaokd != 0) {
                    if (cailiaoname.indexOf("花纹板") > -1 || cailiaoname.indexOf("钢板网") > -1 || cailiaoname.indexOf("钢格板") > -1 || cailiaoname.indexOf("栅格板") > -1 || cailiaoname.indexOf("电焊网") > -1) {
                        cailiaodzh = (cailiaocd * cailiaokd * lilunzhl / 1000000).toFixed(3);
                        Xishu_B_Shape = "1";
                    }
                    else {
                        cailiaodzh = (cailiaoguige * cailiaocd * cailiaokd * lilunzhl / 1000000).toFixed(3);
                    }
                    bgzmy = (cailiaocd * cailiaokd / 1000000).toFixed(3); //材料形状规则
                }
                cailiaozongzhong = (cailiaodzh * shuliang * parseFloat(Xishu_B_Shape)).toFixed(3);
//                if (dzh == 0 || dzh == "")//单重为0或空
//                {
//                    dzh = cailiaodzh;
                //                }
                zongzhong = (cailiaodzh * shuliang).toFixed(3);
            }
            else if (mapishape == "型" || mapishape == "圆")//cailiaoname.indexOf("圆钢")>-1||cailiaoname.indexOf("型钢")>-1||cailiaoname=="扁钢"||cailiaoname.indexOf("焊管")>-1||cailiaoname.indexOf("焊接管")>-1||cailiaoname=="无缝钢管"||cailiaoname=="无缝管"||cailiaoname.indexOf("槽钢")>-1||cailiaoname.indexOf("角钢")>-1||cailiaoname=="工字钢"||cailiaoname=="方钢"||cailiaoname=="方钢管"||cailiaoname=="矩形管"||cailiaoname.indexOf("轨道")>-1
            {
                if (cailiaocd != 0) {

                    cailiaodzh = (cailiaocd * lilunzhl / 1000).toFixed(3);
                    cailiaozongzhong = (cailiaodzh * shuliang * parseFloat(Xishu_X_Shape)).toFixed(3);
                    cailiaozongchang = (cailiaocd * shuliang * parseFloat(Xishu_X_Shape)).toFixed(3);
                    // dzh = cailiaodzh;
                    zongzhong = (cailiaodzh * shuliang).toFixed(3);
                }
                else {


                }
            }
            else if (caigoudw.indexOf("米") > -1 || caigoudw.indexOf("m") > -1 || caigoudw.indexOf("M") > -1 || caigoudw.indexOf("米") > -1 || caigoudw.indexOf("m") > -1 || caigoudw.indexOf("M") > -1) {

                cailiaozongchang = (cailiaocd * shuliang).toFixed(3);
               // dzh = (lilunzhl * cailiaocd / 1000).toFixed(3);
                cailiaodzh = (lilunzhl * cailiaocd / 1000).toFixed(3);
                cailiaozongzhong = (lilunzhl * shuliang * cailiaocd / 1000).toFixed(3);
                zongzhong = (lilunzhl * shuliang * cailiaocd / 1000).toFixed(3);
            }
            else {

                cailiaodzh = (lilunzhl * 1).toFixed(3);
                cailiaozongzhong = (cailiaodzh * shuliang).toFixed(3);
                //  cailiaozongchang = (cailiaocd * shuliang). toFixed(3);
               // dzh = cailiaodzh;
                zongzhong = (dzh * shuliang).toFixed(3);
            }

            tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = cailiaozongzhong;
            if(dzh!=""){
             tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = dzh*shuliang;
            }
          // 
            tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value = cailiaodzh;
            tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value = cailiaozongchang;
            tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value = bgzmy;
            tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[1].value = bgzmy * shuliang * parseFloat(Xishu_B_Shape);
          // tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value = dzh;

            //根据单位，赋值用量
            if (dw == "m2" || dw == "平米") {
                tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = (bgzmy * parseFloat(Xishu_B_Shape) * shuliang).toFixed(3);
                bgzmy = (cailiaocd * cailiaokd / 1000000).toFixed(3); //材料形状规则
            }
            else if (dw == "m" || dw == "米") {
                tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = (parseFloat(cailiaozongchang) / 1000).toFixed(3);
                cailiaozongchang = (cailiaocd * shuliang * parseFloat(Xishu_X_Shape)).toFixed(3);
            }
            else {
                tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = shuliang;
            }
        }
    }
    else {
        if (input.parentNode.cellIndex != 11)//物料编码为空时，输入长宽无效
        {
            if (parseInt(cailiaocd) > 0 || parseInt(cailiaokd) > 0) {
                tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value = "0";
                tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value = "0";
                alert("提示:物料编码为空，输入【材料长度】【材料宽度】无效！！！");
            }
        }
        else//如果输入数量，要修改实际总重
        {
            if (dzh != "") {
                tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = (dzh * shuliang).toFixed(3);
            }
        }
    }
}

//计划数量改变
function autop_shuliang(input) {
    var table = document.getElementById(getClientId().Id1);

    var tr = table.getElementsByTagName("tr");
    var index = input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    var name = tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value;
    var shuliang = tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[1].value.replace(/(\s*$)/g, ""); //总数量
    var p_shuliang = tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[2].value.replace(/(\s*$)/g, ""); //计划数量
    var dzh = tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //实际单重
    var bgzmy = tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    var cailiaocd = tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //长度
    var cailiaodzh = tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //材料单重
    var cailiaozongchang = tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //材料单重
    var dw = tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value; //技术单位
    var fix = tr[index].getElementsByTagName("td")[28].getElementsByTagName("select")[0].value;
    GetXiShu_TMOrg(fix);
    var pattem = /^\d+(\.\d+)?$/; //数量验证
    if (shuliang == "") {
        alert("请先输入单台数量！！！");
        input.value = "";
        return false;
    }
    //数量检查
    if (pattem.test(p_shuliang)) {
        if (parseInt(p_shuliang) < parseInt(shuliang)) {
            alert("【计划数量】不能小于【总数量】！！！");
            input.value = shuliang;
            autop_shuliang(input);
            return false;
        }
        else {

            tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = (cailiaodzh * p_shuliang * parseFloat(Xishu_B_Shape)).toFixed(3);
            tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = (dzh * p_shuliang).toFixed(3);
            if (bgzmy != "") {
                tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[1].value = (bgzmy * p_shuliang * parseFloat(Xishu_B_Shape).toFixed(3)).toFixed(3);
            }
            if (cailiaozongchang != "") {
                tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value = (cailiaocd * p_shuliang * parseFloat(Xishu_B_Shape)).toFixed(3);
            }


            //根据单位，赋值用量
            if (dw == "m2" || dw == "平米") {
                tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = (bgzmy * p_shuliang * parseFloat(Xishu_B_Shape)).toFixed(3);

            }
            else if (dw == "m") {
                tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = (cailiaocd * p_shuliang * parseFloat(Xishu_B_Shape)).toFixed(3);
            }
            else {
                tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = p_shuliang;
            }

        }
    }

}
//实际单重改变
function auto1(input) {
    var table = document.getElementById(getClientId().Id1);
    var tr = table.getElementsByTagName("tr");
    var dzh = document.getElementById(input.id).value;
    var index = input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    var shuliang = tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[1].value;
    var name = tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    var cailiaodz = tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value; //材料单重
    var shape = tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //材料种类
    var llzl = tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //理论重量
    var guige = tr[index].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //材料规格
    var fix = tr[index].getElementsByTagName("td")[28].getElementsByTagName("select")[0].value;
    GetXiShu_TMOrg(fix);

    var pattem = /^\d+(\.\d+)?$/; //实际单重
    var pattem2 = /^[1-9][0-9]*$/; //数量验证
    //实际单重为空不计算
    if (dzh == "") {
        return false;
    }
    //数量为空默认1
    if (shuliang == "" || shuliang == 0) {
        shuliang = "1";
        input.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value = "";
    }

    if (pattem.test(dzh) && pattem.test(shuliang)) {
        //计算总重
        input.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = (dzh * shuliang).toFixed(3);
        //比较实际单重和材料单重
        if (parseFloat(cailiaodz) < parseFloat(dzh)) {
            if (cailiaodz != "0" && cailiaodz != "") {
                alert(cailiaodz);
                alert("实际单重超出材料单重，请核实！！！");
            }
        }
        else {
            if (dzh > 10000) {
                alert('提示:输入【实际单重】超出10吨,请核实！！!');
            }
        }
        //计算面域（对于板材）
        //        if ((shape == "板" && pattem.test(guige)) || name.indexOf("花纹板") > -1 || name.indexOf("钢格板") > -1 || name.indexOf("栅格板") > -1) {
        //            var bgzmy = 0;
        //            if (name.indexOf("花纹板") > -1 || name.indexOf("钢格板") > -1 || name.indexOf("栅格板") > -1) {
        //                bgzmy = (dzh / llzl). toFixed(3);
        //            }
        //            else {
        //                bgzmy = (dzh / (llzl * guige)). toFixed(3);
        //            }
        //            tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value = bgzmy;
        //        }

    }
    else {
        alert('提示:输入【实际单重】或【数量】格式不正确!');
        document.getElementById(input.id).value = 0;
        document.getElementById(input.id).focus();
    }
}

//材料单重改变
function auto2(input) {
    var table = document.getElementById(getClientId().Id1);
    var tr = table.getElementsByTagName("tr");
    var index = input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    var marid = tr[index].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    var cldzh = document.getElementById(input.id).value;
    var lilunzhl = tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    var shuliang = input.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[1].value.replace(/(\s*$)/g, "");

    var p_shuliang = input.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[2].value.replace(/(\s*$)/g, "");

    var dw = input.parentNode.parentNode.getElementsByTagName("td")[16].getElementsByTagName("input")[0].value; //技术单位
    
    var tudzh = input.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //图单重
    var bgmy = input.parentNode.parentNode.getElementsByTagName("td")[18].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    var cailiaoguige = tr[index].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    var blankshape = tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    var fix = tr[index].getElementsByTagName("td")[28].getElementsByTagName("select")[0].value;
    GetXiShu_TMOrg(fix);

    var pattem = /^\d+(\.\d+)?$/; //材料单重
    var pattem2 = /^[1-9][0-9]*$/; //数量验证
    if (cldzh == "") {
        return false;
    }
    else {
        if (marid == "") {
            alert("提示:物料编码为空，部件输入【材料单重】无效！！！");
            document.getElementById(input.id).value = "0";
            return false;
        }
    }

    if (shuliang == "" || shuliang == "0") {
        shuliang = "1";
        input.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value = "1";
    }

    if (pattem.test(cldzh) && pattem.test(shuliang)) {
        if (cldzh != 0) {
            //材料总重
            if (blankshape.indexOf("板") > -1) {
                input.parentNode.parentNode.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = (cldzh * p_shuliang * parseFloat(Xishu_B_Shape)).toFixed(3);
            }
            else if (blankshape.indexOf("型") > -1 || blankshape == "圆") {
                input.parentNode.parentNode.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = (cldzh * p_shuliang * parseFloat(Xishu_X_Shape)).toFixed(3);
                //材料长度
                input.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value = (cldzh * 1000 / lilunzhl).toFixed(0);
                //材料用量
                if (dw == "m" || dw == "米") 
                {
                     input.parentNode.parentNode.getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = (cldzh * p_shuliang * parseFloat(Xishu_X_Shape)/ lilunzhl).toFixed(3);
                }
                //材料总长
                input.parentNode.parentNode.getElementsByTagName("td")[19].getElementsByTagName("input")[0].value = (cldzh * p_shuliang * parseFloat(Xishu_X_Shape) * 1000 / lilunzhl).toFixed(3);
            }
            else {
                input.parentNode.parentNode.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = (cldzh * p_shuliang).toFixed(3);
            }

            if (tudzh == "" || tudzh == 0)//如果实际单重为空或0，重新计算
            {
                //实际单重
                input.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value = cldzh;
                //总重
                input.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = (cldzh * shuliang).toFixed(3);
            }
            if (cldzh > 10000) {
                alert('提示:输入【材料单重】超出10吨,请核实！！!');
            }
        }

    }
    else {
        alert('提示:输入【材料单重】或【数量】格式不正确!');
        document.getElementById(input.id).value = "0";
        document.getElementById(input.id).focus();
    }
}

//面域改变
function automy(input) {
    var p_shuliang = input.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[2].value;
    var my = document.getElementById(input.id).value;
    var table = document.getElementById(getClientId().Id1);
    var tr = table.getElementsByTagName("tr");
    var index = input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    var cailiaocd = tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //长度
    var cailiaokd = tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //宽度
    var cailiaoname = tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value;
    var marid = tr[index].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    var cailiaoguige = tr[index].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
     if(cailiaoguige.indexOf("+")>-1){
       cailiaoguige=parseFloat(cailiaoguige.split('+')[0])+parseFloat(cailiaoguige.split('+')[1]);
       }
    
    var lilunzhl = tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    var dw = tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value; //技术单位
    var marshape = tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //毛坯形状
    var p_mianyu = tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[1].value.replace(/(\s*$)/g, "");
    var miany = (cailiaocd * cailiaokd / 1000000).toFixed(3);
    var fix = tr[index].getElementsByTagName("td")[28].getElementsByTagName("select")[0].value;

    GetXiShu_TMOrg(fix);

    var pattem = /^\d+(\.\d+)?$/; //面域
    var pattem2 = /^[1-9][0-9]*$/; //数量验证
    if (!pattem.test(my)) {
        alert("请输入正确的面域数值！！！")
        input.value = "";
        return false;
    }

    if (my == "") {
        my = "0";
    }

    if (marid == "" && my != "") {
        alert("提示:物料编码为空，部件输入【面域】无效！！！");
        document.getElementById(input.id).value = "0";
        return false;
    }

    //数量赋值
    if (p_shuliang == "" || p_shuliang == 0) {
        p_shuliang = "1";
        input.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value = "";
    }
    if (pattem.test(my) && pattem.test(p_shuliang)) {
//        if (parseFloat(my) > parseFloat(miany) & marshape == "板" & cailiaocd != "0" & cailiaokd != "0" & cailiaocd != "" & cailiaokd != "") {
//            alert("该条物料毛坯形状为【板】，输入面域超出规则长、宽下面域，输入无效！！！");
//            document.getElementById(input.id).value = miany;
//            return false;
//        }
        if (parseFloat(my) > 100) {
            alert("提示:输入【面域】大于100平方米，请核实！！！");
        }
        var bgzmy = my; //单个面域
        if (sub_marid != "01.01") //STR标准件
        {
            if (marshape == "板" || cailiaoname.indexOf("钢板") > -1 || cailiaoname.indexOf("钢格板") > -1 || cailiaoname.indexOf("栅格板") > -1 || cailiaoname.indexOf("花纹板") > -1)//
            {
                if (bgzmy == 0) {
                    if (cailiaocd != 0 && cailiaokd != 0) {
                        if (cailiaoname.indexOf("花纹板") > -1 || cailiaoname.indexOf("钢格板") > -1 || cailiaoname.indexOf("钢板网") > -1 || cailiaoname.indexOf("栅格板") > -1) {
                            cailiaodzh = (cailiaocd * cailiaokd * lilunzhl / 1000000).toFixed(3);
                            if (cailiaoname.indexOf("钢格板") > -1) {
                                Xishu_B_Shape = "1";
                            }
                        }
                        else {
                            cailiaodzh = (cailiaoguige * cailiaocd * cailiaokd * lilunzhl / 1000000).toFixed(3);
                        }
                        cailiaozongzhong = (cailiaodzh * p_shuliang * parseFloat(Xishu_B_Shape)).toFixed(3);
                        bgzmy = cailiaocd * cailiaokd / 1000000; //材料形状规则

                        tudzh = cailiaodzh;

                        zongzhong = (tudzh * p_shuliang).toFixed(3);

                    }
                }
                else {  //面域不为0
                    if (cailiaoname.indexOf("花纹板") > -1 || cailiaoname.indexOf("栅格板") > -1 || cailiaoname.indexOf("钢格板") > -1) {
                        cailiaodzh = (bgzmy * lilunzhl).toFixed(3);
                    }
                    else {
                        cailiaodzh = (cailiaoguige * bgzmy * lilunzhl).toFixed(3);
                    }
                    cailiaozongzhong = (cailiaodzh * p_shuliang * parseFloat(Xishu_B_Shape)).toFixed(3);
                    tudzh = cailiaodzh;
                    zongzhong = (tudzh * p_shuliang).toFixed(3);
                }

            }
        }
        else {
            //如果是标准件

        }

      //  tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value = tudzh;
        tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value = cailiaodzh;
        tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = cailiaozongzhong;
       // tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = tudzh * p_shuliang;
        tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[1].value = (parseFloat(bgzmy) * parseFloat(Xishu_B_Shape) * p_shuliang).toFixed(3);


        //根据单位，赋值用量
        if (dw == "m2" || dw == "平米") {
            tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = (bgzmy * parseFloat(Xishu_B_Shape) * p_shuliang).toFixed(3);

        }
        else if (dw == "m") {
            tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = (parseFloat(cailiaozongchang) / 1000).toFixed(3);
        }
        else {
            tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = p_shuliang;
        }

    }

}
//图纸上单重计算
function TudanZhong(input) {
    var tudz = document.getElementById(input.id).value;
    var shuliang = tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[1].value;
    var pattem = /^\d+(\.\d+)?$/;
    if (!pattem.test(tudz)) {
        document.getElementById(input.id).value = "";
        alert("提示:输入【图纸上单重】格式不正确！！！");
    }
    else {
        tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = (parseFloat(tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value) * shuliang).toFixed(3);
    }

}

//图纸上单重计算
function TudanZhongBom(input) {
    var table = document.getElementById(getClientId().Id1);


    var tr = table.getElementsByTagName("tr");
    var index = input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    var number = tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[1].value;
    var tudz = document.getElementById(input.id).value;
    var real = tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
    var pattem = /^\d+(\.\d+)?$/;
    if (!pattem.test(tudz)) {
        document.getElementById(input.id).value = "";
        alert("提示:输入【图纸上单重】格式不正确！！！");
        return;
    }
    //图纸总重
    tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = (parseFloat(input.value) * number).toFixed(3);

    if (document.getElementById("ctl00_PrimaryContent_ckbUnitWght").checked) {

        if (number == "") {
            number = 1;
        }
        //实际单重=图纸上单重
        real = input.value;
        tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value = real;
        //总重
        tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value = (parseFloat(real) * number).toFixed(3);
    }
    CheckUnitWght_TuUnit(input);
}

function CheckUnitWght_TuUnit(input) {
    var table = document.getElementById(getClientId().Id1);
    var tr = table.getElementsByTagName("tr");
    var index = input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;

    var obj_real = tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0];
    var obj_tu = tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0];

    var real = obj_real.value;
    var tudz = obj_tu.value;

    if (real == "") {
        real = 0;
    }

    if (tudz == "") {
        tudz = 0;
    }

    if ((Math.abs(real - tudz) > 0.01 * tudz)) {
        obj_real.style.background = "yellow";
        obj_tu.style.background = "yellow";
    }
    else {
        obj_real.style.background = "white";
        obj_tu.style.background = "white";
    }
}

//材料总重改变        
function MarTotalWeightChange(obj) {
    var checktxt = obj.value;
    var pattem = /^\d+(\.\d+)?$/;
    var table = document.getElementById(getClientId().Id1);
    var tr = table.getElementsByTagName("tr");
    var index = obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    var cailiaodz = tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    var shuliang = tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    if (!pattem.test(checktxt)) {
        alert("请输入正确的数值格式！！！");
        obj.value = "";
    }
}

//控制定尺是否可用
function CtrlFixSize(obj) {
    var table = document.getElementById(getClientId().Id1);
    var tr = table.getElementsByTagName("tr");
    var index = obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    var bancai = tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value;
    var fixsize = tr[index].getElementsByTagName("td")[28].getElementsByTagName("select")[0].value;
    var bgzmy = tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value;
    var dw = tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value; //技术单位
    var caigoudw = tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value; //采购单位
    var marid = tr[index].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    if (marid != "") {
        var shuliang = tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[2].value.replace(/(\s*$)/g, ""); //数量
        var clcd = tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
        var cldzh = tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value;

        if (fixsize == "Y") {
            //材料总重
            tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = (cldzh * shuliang).toFixed(3);
            if (bancai == "型" || bancai == "圆" || caigoudw=="米" || caigoudw=="m" || caigoudw=="M") {
                //材料总长
                tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value = (clcd * shuliang).toFixed(3);

                //单位
                //                tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value = "根";
                //材料用量
                tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = (clcd * shuliang / 1000).toFixed(3);

            }
            else if (bancai == "板") {
                //单位
               // tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value = "张";
                tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[1].value = (parseFloat(bgzmy) * shuliang).toFixed(3);
                //材料用量
                tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = (parseFloat(bgzmy) * shuliang).toFixed(3);
            }
            tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = (cldzh * shuliang).toFixed(3);
           
        }
        else {
            GetXiShu_TMOrg(fixsize);
            if (bancai == "型" || bancai == "圆" || caigoudw == "米" || caigoudw == "m" || caigoudw == "M") {
                //材料总重
                tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = (cldzh * shuliang * parseFloat(Xishu_X_Shape)).toFixed(3);

                //材料总长
                tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value = (clcd * shuliang * parseFloat(Xishu_X_Shape)).toFixed(3);
                //单位
                tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value = "m";
                //材料用量
                tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = (clcd * shuliang * parseFloat(Xishu_X_Shape)/1000).toFixed(3);

            }
            else if (bancai == "板") {
                //材料总重
                tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = (cldzh * shuliang * parseFloat(Xishu_B_Shape)).toFixed(3);
                tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[1].value = (parseFloat(bgzmy) * shuliang * parseFloat(Xishu_B_Shape)).toFixed(3);
                //单位
                tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value = "m2";
                //材料用量
                tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value = (parseFloat(bgzmy) * shuliang * parseFloat(Xishu_B_Shape)).toFixed(3);
            }
            else {
                //材料总重
                tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value = (cldzh * shuliang).toFixed(3);
            }
        }
    }
}

//毛坯形状改变
function ChangeofMarShape(obj) {
    var table = document.getElementById(getClientId().Id1);
    var tr = table.getElementsByTagName("tr");
    var index = obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    var fixsize = tr[index].getElementsByTagName("td")[28].getElementsByTagName("select")[0].value;
    var marid = tr[index].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
    if (marid != "") {
        var input = tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0];
        auto(input);
    }
}



/***************************************************
↑↓←→键控制，增加了对隐藏列及无法获取焦点列的控制
****************************************************/
//Table的←↑→↓控制
function grControlFocus(input) {
    var e = event.srcElement;
    //    var e = event.target;
    var rowIndex = e.parentNode.parentNode.rowIndex; //获取行号
    //var cellIndex=e.parentNode.cellIndex;  //获取焦点的列号
    var tr = e.parentNode.parentNode.parentNode.getElementsByTagName("tr"); //获取行
    var rowcount = tr.length;  //行数
    var td = e.parentNode.parentNode.getElementsByTagName("td"); //获取行的单元格
    var cellcount = td.length;    //列数
    /////  alert('共'+cellcount+'列;当前列'+cellIndex);
    ///// alert(td);
    /////var hmtlid=input.id.substring(input.id.lastIndexOf('_'),input.id.length); 
    /////alert(hmtlid);
    var cellIndex = GetRealCellIndex(input.id);
    /////alert(cellIndex);
    var key = window.event.keyCode;   //获得按钮的编号


    if (key == 37)   //向左 
    {
        //是否为第一列
        for (var i = cellIndex - 1; i > 0; i--) {
            //alert(i);
            if (tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0] == null || tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].readOnly || tr[rowIndex].getElementsByTagName("td")[i].className == "hidden") {
                continue;
            }
            else {
                tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].select();
                tr[rowIndex].style.backgroundColor = '#55DF55';
                break;
            }
        }
    }

    if (key == 38)  //向上
    {
        for (var i = rowIndex - 1; i > 0; i--) {
            if (tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0] != null || tr[i].getElementsByTagName("td")[cellIndex].className == 'hidden') {
                tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].select();
                tr[rowIndex].style.backgroundColor = '#EFF3FB';
                tr[i].style.backgroundColor = '#55DF55';
                break;
            }
            else {
                continue;
            }
        }
    }

    if (key == 39)  //向右
    {
        for (var i = cellIndex + 1; i < cellcount; i++) {
            if (i < cellcount) {
                if (tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0] == null || tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].readOnly || tr[rowIndex].getElementsByTagName("td")[i].className == 'hidden')//
                {
                    continue;
                }
                else {
                    tr[rowIndex].style.backgroundColor = '#55DF55';

                    tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].focus();
                    tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].select();
                    break;
                }
            }
            else {
                break;
            }
        }
    }

    if (key == 40)   //向下
    {
        for (var i = rowIndex + 1; i < rowcount; i++) {
            if (tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0] != null || tr[i].getElementsByTagName("td")[cellIndex].className == 'hidden') {
                tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].select();
                tr[rowIndex].style.backgroundColor = '#EFF3FB'; //原来的行变回原来的颜色
                tr[i].style.backgroundColor = '#55DF55'; //下一行变色
                break;
            }
            else {
                continue;
            }
        }
    }
}

function GetRealCellIndex(id) {
    var retvalue = 0;
    if (id.indexOf("_ch_name") > -1) {
        retvalue = 5;
    }
    else if (id.indexOf("_en_name") > -1) {
        retvalue = 32;
    }
    else {
        var hmtlid = id.substring(id.lastIndexOf('_'), id.length);
        switch (hmtlid) {
            case "_tuhao": retvalue = 2; break;
            case "_zongxu": retvalue = 3; break;
            case "_marid": retvalue = 4; break;
            case "_ch_name": retvalue = 5; break;
            case "_cailiaoguige": retvalue = 6; break;
            case "_caizhi": retvalue = 7; break;
            case "_cailiaocd": retvalue = 8; break;
            case "_cailiaokd": retvalue = 9; break;
            case "_note": retvalue = 10; break;
            case "_shuliang": retvalue = 11; break;
            case "_tudz": retvalue = 12; break;

            case "_tuzhiZZ": retvalue = 13; break;
            case "_cailiaodzh": retvalue = 14; break;
            case "_cailiaozongzhong": retvalue = 15; break;
            case "_labunit": retvalue = 16; break;
            case "_txtYongliang": retvalue = 17; break;
            case "_bgzmy": retvalue = 18; break;
            case "_cailiaozongchang": retvalue = 19; break;
            case "_cailiaoType": retvalue = 20; break;
            case "_xialiao": retvalue = 21; break;
            case "_process": retvalue = 22; break;
            case "_zongbeizhu": retvalue = 23; break;
            case "_lilunzhl": retvalue = 24; break;
            case "_biaozhun": retvalue = 25; break;
            case "_caigoudanwei": retvalue = 26; break;

            case "_ddlKeyComponents": retvalue = 27; break;
            case "_ddlFixedSize": retvalue = 28; break;
            case "_ddlWmp": retvalue = 29; break;


            default: break;
        }
    }
    return retvalue;
}


function getSelect(obj) {
    var objtr = obj.parentNode.parentNode;
    objtr.style.backgroundColor = '#55DF55';

}

function Fast_Op(obj) {
    var control_id = obj.id;
    var tuhao = document.getElementById("ctl00_PrimaryContent_ckbTuhao");
    var jztuhao = document.getElementById("ctl00_PrimaryContent_ckbJZTuhao");
    var xuhao = document.getElementById("ctl00_PrimaryContent_ckbXuhao");
    var jzxuhao = document.getElementById("ctl00_PrimaryContent_ckbJZXuhao");
    var flag;
    if (control_id == "ctl00_PrimaryContent_ckbTuhao" || control_id == "ctl00_PrimaryContent_ckbJZTuhao") {
        flag = "0";
    }
    else if (control_id == "ctl00_PrimaryContent_ckbXuhao" || control_id == "ctl00_PrimaryContent_ckbJZXuhao") {
        flag = "1";
    }

    switch (flag) {
        case "0":
            if (obj.checked) {
                tuhao.checked = false;
                jztuhao.checked = false;
                obj.checked = true;
            }
            break;
        case "1":
            if (obj.checked) {
                xuhao.checked = false;
                jzxuhao.checked = false;
                obj.checked = true;
            }
            break;
        default:
            break;
    }

    var note = "当前:";
    if (tuhao.checked) {
        note += "图";
    }
    if (jztuhao.checked) {
        note += "前图";
    }
    if (xuhao.checked) {
        if (note == "当前:") {
            note += "序";
        }
        else {
            note += "+序";
        }
    }
    if (jzxuhao.checked) {
        if (note == "当前:") {
            note += "前序";
        }
        else {
            note += "+前序";
        }
    }
    if (note == "当前:") {
        note = "(" + note + "无)";
    }
    else {
        note = "(" + note + ")";
    }

    document.getElementById("ctl00_PrimaryContent_lblshortcut").value = note;
}

//BOM录入界面图号的两种操作方式
function OrgAutoTuHao(obj) {
    if (document.getElementById("ctl00_PrimaryContent_ckbTuhao").checked) {
        if (obj.value != "") {
            var table = document.getElementById(getClientId().Id1);
            var tablerows = table.rows.length;
            var tr = table.getElementsByTagName("tr");
            var index = obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var t = parseInt(index) + 1;
            if (t < tablerows) {
                if (table.rows[t].cells[2].getElementsByTagName("input")[0].value == "") {
                    table.rows[t].cells[2].getElementsByTagName("input")[0].value = obj.value;
                }
            }
        }
    }
    else if (document.getElementById("ctl00_PrimaryContent_ckbJZTuhao").checked) {
        var jz = document.getElementById("ctl00_PrimaryContent_txtJZTuhao").value;
        if (jz != "" && obj.value != "") {
            obj.value = jz + "-" + obj.value;
        }
    }
}

function OrgAutoXuhao(obj) {
    if (document.getElementById("ctl00_PrimaryContent_ckbJZXuhao").checked) {
        var jz = document.getElementById("ctl00_PrimaryContent_txtJZXuhao").value;
        if (jz != "" && obj.value != "") {
            obj.value = jz + "." + obj.value;
        }

        // verify(obj);
    }
    else if (document.getElementById("ctl00_PrimaryContent_ckbXuhao").checked) {
        if (obj.value != "") {
            var table = document.getElementById(getClientId().Id1);
            var tablerows = table.rows.length;
            var tr = table.getElementsByTagName("tr");
            var index = obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var t = parseInt(index) + 1;
            if (t < tablerows) {
                if (table.rows[t].cells[3].getElementsByTagName("input")[0].value == "") {
                    var oldzongxu = obj.value;
                    var shuzu = oldzongxu.split('.');
                    var back = shuzu[shuzu.length - 1];
                    var length = back.length;
                    var newzongxu = oldzongxu.substr(0, oldzongxu.length - length) + (parseInt(back) + 1);

                    table.rows[t].cells[3].getElementsByTagName("input")[0].value = newzongxu;
                }
            }
        }
    }
}
//BOM录入界面数据的重复性提提示
function BomInputCheck() {
    var btnSave = document.getElementById('ctl00_PrimaryContent_btnsave');
    btnSave.Enabled = false;
    var table = document.getElementById(getClientId().Id1);
    var tablerows = table.rows.length;
    var array_zongxu = new Array(new Array());
    var array_index = 0;

    for (var i = 1; i < tablerows; i++) {
        var zx = table.rows[i].cells[3].getElementsByTagName("input")[0].value;
        var marid = table.rows[i].cells[4].getElementsByTagName("input")[0].value;
        var name = table.rows[i].cells[5].getElementsByTagName("input")[0].value;
        if (zx != "") {
            array_zongxu[array_index] = new Array();
            array_zongxu[array_index][0] = zx;
            array_zongxu[array_index][1] = marid;
            array_zongxu[array_index][2] = name;
            array_index++;
        }
    }
    if (array_index > 1) {
        for (var m = 0; m < array_index - 1; m++) {
            for (var n = m + 1; n < array_index; n++) {
                if (array_zongxu[m][0] == array_zongxu[n][0]) {
                    if (array_zongxu[m][1] != array_zongxu[n][1]) {
                        alert("提示:无法保存！！！\r\r页面上相同总序【" + array_zongxu[m][0] + "】的物料编码不同！！！");
                        return false;
                    }

                    var zongxu_same = true;
                    if (array_zongxu[m][0] == array_zongxu[n][0]) {
                        zongxu_same = confirm("总序【" + array_zongxu[m][0] + "】有多条记录！！！\r\r确认继续吗？");
                        if (zongxu_same) {
                            if ((array_zongxu[m][1] == array_zongxu[n][1]) && (array_zongxu[m][2] != array_zongxu[n][2])) {
                                var yes = confirm("页面上总序【" + array_zongxu[m][0] + "】有多条，名称不同！！！\r\r确认继续保存吗？？？");
                                if (yes == false) {
                                    return false;
                                }
                            }
                        }
                        else {
                            return false;
                        }
                    }
                }
            }
        }
    }
    //存在物料编码的毛坯形状不能为空
    var marid = "";
    var mapxingzhuang = "";
    var danwei = "";
    var cailiaozongchang = "";
    for (var i = 1; i < tablerows; i++) {
        marid = table.rows[i].cells[4].getElementsByTagName("input")[0].value;
        mapxingzhuang = table.rows[i].cells[20].getElementsByTagName("input")[0].value;
        danwei = table.rows[i].cells[16].getElementsByTagName("input")[0].value;
        cailiaozongchang = table.rows[i].cells[19].getElementsByTagName("input")[0].value;
        if (marid != "" && mapxingzhuang == "") {
            alert("第" + i + "行【材料类型】为空，请输入！！！");
            table.rows[i].cells[20].getElementsByTagName("input")[0].focus();
            table.rows[i].cells[20].getElementsByTagName("input")[0].style.background = "yellow";
            return false;
        }

        if ((danwei.indexOf("(米-") > -1 || danwei.indexOf("-米)") > -1) && (cailiaozongchang == "" || cailiaozongchang == "0")) {
            alert("第" + i + "行物料的【材料总长】为空，请输入！！！\r\r提示:该物料采购单位或辅助单位为\"米\"");
            table.rows[i].cells[19].getElementsByTagName("input")[0].focus();
            table.rows[i].cells[19].getElementsByTagName("input")[0].style.background = "yellow";
            return false;
        }
        table.rows[i].cells[20].getElementsByTagName("input")[0].style.background = "white";
        table.rows[i].cells[19].getElementsByTagName("input")[0].style.background = "white";
    }
    return CheckNumNotZeroWithPurUnit();
}

//BOM录入界面更多操作显示
function Show_div_othersetting(obj) {
    if (document.getElementById("ctl00_PrimaryContent_ckbQYXH").checked) {
        var table = document.getElementById(getClientId().Id1);
        var tablerows = table.rows.length;
        for (var t = 1; t < tablerows; t++) {
            table.rows[t].cells[1].getElementsByTagName("div")[0].className = "hidden";
        }
        var currentRowIndex = obj.parentNode.parentNode.rowIndex;
        var show_div = table.rows[currentRowIndex].cells[1].getElementsByTagName("div")[0];
        show_div.className = "show";
    }
}

//验证序号格式
function CheckXuHao(obj) {
    var xuhao = obj.value;
    if (xuhao != "") {
        var pattem = /^1\.0\.([1-9]{1}([0-9]){0,}){1}$|1((\.[1-9]{1}[0-9]{0,}){1,})$/;
        if (!pattem.test(xuhao)) {
            alert("请输入正确的【序号】格式！！！");
            obj.value = "";
        }
    }
}

function GetXiShu_TMOrg(fixsize) {
    if (fixsize == "Y") {
        Xishu_B_Shape = "1";
        Xishu_X_Shape = "1";
    }
    else {
        Xishu_B_Shape = document.getElementById(getClientId().BXishu).value;
        Xishu_X_Shape = document.getElementById(getClientId().XXishu).value;
    }


}





//根据采购主单位验证重量、长度、数量等不能为空
function CheckNumNotZeroWithPurUnit() {
    var _tablename;
    var _tablerows
    var _marid;
    var _dw;
    var _singnumber;
    var _number;
    var _p_number;
    var _cailiaozhongchang;
    var _cailiaozhongzhong;
    var _mpmy;


    _tablename = document.getElementById(getClientId().Id1);
    _tablerows = _tablename.rows.length;
    for (var i = 1; i < _tablerows; i++) {
        _marid = _tablename.rows[i].cells[4].getElementsByTagName("input")[0].value;
        if (_marid != "") {
            _dw = _tablename.rows[i].cells[26].getElementsByTagName("input")[0].value
            //采购主单位为长度单位：米
            if (_dw.indexOf("(米") > -1 || _dw.indexOf("(M") > -1 || _dw.indexOf("(m") > -1) {
                _cailiaozhongchang = _tablename.rows[i].cells[19].getElementsByTagName("input")[0].value;
                if (_cailiaozhongchang == "" || parseFloat(_cailiaozhongchang) <= 0) {
                    alert("第" + i + "行:采购主单位为【米】,必须输入【材料长度】及【材料总长】！！！");
                    return false;
                }
            }
            //采购主单位为重量单位：kg 、T
            else if (_dw.indexOf("(T") > -1 || _dw.indexOf("(t") > -1 || _dw.indexOf("(吨") > -1 || _dw.indexOf("(kg") > -1 || _dw.indexOf("(KG") > -1 || _dw.indexOf("(千克") > -1 || _dw.indexOf("(Kg") > -1 || _dw.indexOf("(kG") > -1 || _dw.indexOf("(公斤") > -1) {
                _cailiaozhongzhong = _tablename.rows[i].cells[15].getElementsByTagName("input")[0].value;
                if (_cailiaozhongzhong == "" || parseFloat(_cailiaozhongzhong) <= 0) {
                    alert("第" + i + "行:采购主单位为重量单位,必须输入【材料单重】及【材料总重】！！！");
                    return false;
                }
            }
            else if (_dw.indexOf("(平方米") > -1 || _dw.indexOf("(平米") > -1 || _dw.indexOf("(m2") > -1 || _dw.indexOf("(M2") > -1 || _dw.indexOf("(㎡") > -1) {
                _mpmy = _tablename.rows[i].cells[18].getElementsByTagName("input")[1].value;
                if (_mpmy == "" || parseFloat(_mpmy) <= 0) {
                    alert("第" + i + "行:采购主单位为面积单位,必须输入【计划面域】！！！");
                    return false;
                }
            }
            //采购单位为其它单位
            else {
                _p_number = _tablename.rows[i].cells[11].getElementsByTagName("input")[2].value;
                if (_p_number == "" || parseFloat(_p_number) <= 0) {
                    alert("第" + i + "行:请输入数量！！！");
                    return false;
                }
            }

            _singnumber = _tablename.rows[i].cells[11].getElementsByTagName("input")[0].value;
            _number = _tablename.rows[i].cells[11].getElementsByTagName("input")[1].value;
            _p_number = _tablename.rows[i].cells[11].getElementsByTagName("input")[2].value;
            if ((parseFloat(_p_number) < parseFloat(_singnumber)) || (parseFloat(_number) < parseFloat(_singnumber))) {
                alert("第" + i + "行:总数量或计划数量少于单台数量，请核实！！！");
                return false;
            }

        }
    }

    return true;
}



