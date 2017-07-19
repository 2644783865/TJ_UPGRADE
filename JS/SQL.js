var conn;
var objrs;
var comText;
var sub_marid;
var tsaid;
var Xishu_B_Shape;
var Xishu_X_Shape;


/*助记码修改*/
function modifyCode() {
    var marid = document.getElementById("marid").value;
    if (marid != "" && marid.substring(0, 1) == "0") {
        if (marid.indexOf(" ") > -1) {
            sub_marid = marid.substring(0, marid.indexOf(" "));
        }
        else {
            sub_marid = marid;
        }
        var xiaolei = sub_marid.substring(0, 5);
        var dalei = xiaolei.substring(0, 2);
        document.getElementById("marid").value = sub_marid;
        //所有清空
        ///////document.getElementById("cailiaoname").value="";
        ////document.getElementById("cailiaoguige").value="";
        document.getElementById("cailiaoguige").value = "";
        document.getElementById("cailiaocd").value = "0";
        document.getElementById("cailiaokd").value = "0";
        document.getElementById("shuliang").value = "1";
        document.getElementById("cailiaozongchang").value = "0";
        document.getElementById("lilunzhl").value = "0";
        document.getElementById("techUnit").value = "";
        document.getElementById("cailiaodzh").value = "0";
        document.getElementById("cailiaozongzhong").value = "0";
        document.getElementById("bgzmy").value = "0";
        document.getElementById("my").value = "0";
        document.getElementById("txtTuDz").value = "0";
        document.getElementById("txtTuZz").value = "0";
        document.getElementById("cailiaoType").value = "";
        //        document.getElementById("zhuangtai").value = "";
        var marids = marid.split('|');
        if (marids.length < 8) { return; }
        document.getElementById("marid").value = marids[0];
        document.getElementById("cnname").value = marids[1];
        document.getElementById("cailiaoguige").value = marids[2];
        document.getElementById("caizhi").value = marids[3];
        document.getElementById("techUnit").value =  marids[4];
        document.getElementById("lilunzhl").value = marids[8];
        document.getElementById("biaozhun").value = marids[9];


        if (marid.indexOf(" ") > -1) {
            sub_marid = marid.substring(0, marid.indexOf(" "));
        }
        else {
            sub_marid = marid;
        }
        var child_marid = sub_marid.substring(0, 5);
        var son_marid = child_marid.substring(0, 2);
        var cname = marids[1];
        //            tr[index].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value = sub_marid;
        if (child_marid == "01.01")//标准件
        {
            document.getElementById("cailiaoType").value = "采";
        }
        if (marids[1].indexOf("钢板") > -1 || marids[1].indexOf("钢格板") > -1 || marids[1].indexOf("花纹板") > -1 || marids[1].indexOf("电焊网") > -1) {
            document.getElementById("cailiaoType").value = "板";
            document.getElementById("techUnit").value = "m2";
          
        }
        else if (marids[1].indexOf("圆") > -1) {
        document.getElementById("cailiaoType").value = "圆";
        document.getElementById("techUnit").value = "m";
        }
        else if (marids[1].indexOf("重轨") > -1 || marids[1].indexOf("钢轨") > -1 || marids[1].indexOf("轻轨") > -1 || marids[1].indexOf("型钢") > -1 || marids[1].indexOf("扁钢") > -1 || marids[1].indexOf("焊管") > -1 || marids[1].indexOf("焊接管") > -1 || marids[1].indexOf("无缝管") > -1 || marids[1].indexOf("无缝钢管") > -1 || marids[1].indexOf("槽钢") > -1 || marids[1].indexOf("角钢") > -1 || marids[1].indexOf("工字钢") > -1 || marids[1].indexOf("方钢") > -1 || marids[1].indexOf("矩形管") > -1 || marids[1].indexOf("轨道") > -1 || marids[1].indexOf("管") > -1 || marids[1].indexOf("铜棒") > -1) {
        document.getElementById("cailiaoType").value = "型";
        document.getElementById("techUnit").value = "m";
        }

    }
}


/*js修改计算*/
function modifyCalculation(n) {
    var text = n;
    var pattem = /^\d+(\.\d+)?$/;
    var format = /^\d+$/;
    var length = document.getElementById("cailiaocd").value;
    var width = document.getElementById("cailiaokd").value;
    var num = document.getElementById("sing_shuliang").value;
    var p_num = document.getElementById("p_shuliang").value;
    ///////alert(p_num);
    document.getElementById("shuliang").value = num * parseFloat(document.getElementById("taishu").value);
    num = document.getElementById("shuliang").value;
    if (n == 3) {
        document.getElementById("p_shuliang").value = num;
        p_num = num;
    }

    switch (text) {
        case 1: //长度
            if (pattem.test(length)) {
                length = document.getElementById("cailiaocd").value;
            }
            else {
                alert('输入格式有误!');
                document.getElementById("cailiaocd").value = 0;
                length = 0;
            }
            break;
        case 2: //宽度
            if (pattem.test(width)) {
                width = document.getElementById("cailiaokd").value;
            }
            else {
                alert('输入格式有误!');
                document.getElementById("cailiaokd").value = 0;
                width = 0;
            }
            break;
        case 3: // 数量
            if (pattem.test(num)) {
                document.getElementById("txtTuZz").value = (num * parseFloat(document.getElementById("txtTuDz").value)).toFixed(2);

            }
            else {
                alert('输入格式有误!');
                document.getElementById("sing_shuliang").value = 1;
                document.getElementById("shuliang").value = parseFloat(document.getElementById("taishu").value);
                num = parseFloat(document.getElementById("taishu").value);
            }
            break;
        default:
            break;
    }

    if (pattem.test(length) && pattem.test(width) && pattem.test(num)) {
        var cailiaoname = document.getElementById("cnname").value;
        var cailiaoguige = document.getElementById("cailiaoguige").value;
        var unit = document.getElementById("techUnit").value; //单位
        var caigoudw = unit;
        var mashape = document.getElementById("cailiaoType").value; //毛坯形状
        var lilunzhl = document.getElementById("lilunzhl").value;
        var bgzmy = document.getElementById("my").value;
        var dzh = document.getElementById("txtTuDz").value;
        sub_marid = document.getElementById("marid").value;
        var xiaolei = sub_marid.substring(0, 5);
        var dalei = xiaolei.substring(0, 2);
        GetXiShu_TMSQL();

        if (sub_marid != "") {
            if (caigoudw.indexOf("(平米") > -1 || caigoudw.indexOf("(平方米") > -1 || caigoudw.indexOf("(M2") > -1 || caigoudw.indexOf("(m2") > -1 || caigoudw.indexOf("(m2") > -1) {
                document.getElementById("my").value = length * width / 1000000;
            }
        }

        if (xiaolei != "01.01") {
            //板材
            if (cailiaoname.indexOf("钢板") > -1 || cailiaoname.indexOf("钢格板") > -1 || cailiaoname.indexOf("栅格板") > -1 || cailiaoname.indexOf("钢板网") > -1 || cailiaoname.indexOf("花纹板") > -1 || mashape == "板") {
                if (length != 0 && width != 0) {

                    if (cailiaoname.indexOf("花纹板") > -1 || cailiaoname.indexOf("钢格板") > -1 || cailiaoname.indexOf("栅格板") > -1 || cailiaoname.indexOf("钢板网") > -1) {
                        document.getElementById("cailiaodzh").value = (length * width * lilunzhl / 1000000).toFixed(2);
                        Xishu_B_Shape = "1";
                    }
                    else {
                        document.getElementById("cailiaodzh").value = (cailiaoguige * length * width * lilunzhl / 1000000).toFixed(2);
                    }
                    document.getElementById("cailiaozongzhong").value = (document.getElementById("cailiaodzh").value * p_num * parseFloat(Xishu_B_Shape)).toFixed(2);
                    document.getElementById("txtTuDz").value = document.getElementById("cailiaodzh").value;
                    document.getElementById("my").value = length * width / 1000000; //材料形状规则
                    document.getElementById("bgzmy").value = length * width * parseFloat(Xishu_B_Shape) * p_num / 1000000; //材料形状规则
                    var zongzhong = document.getElementById("txtTuDz").value;

                    document.getElementById("txtTuZz").value = parseFloat(zongzhong) * p_num;
                }
                else {
                    //材料总重
                    var cldz = document.getElementById("cailiaodzh").value;
                    document.getElementById("cailiaozongzhong").value = (cldz * p_num * parseFloat(Xishu_B_Shape)).toFixed(2);
                    //总重
                    var dz = document.getElementById("txtTuDz").value;
                    document.getElementById("txtTuZz").value = (dz * num).toFixed(2);
                    if (bgzmy != '') { document.getElementById("bgzmy").value = (bgzmy * p_num * parseFloat(Xishu_B_Shape)).toFixed(2); }
                }
                if (document.getElementById("rblSFDC_1").checked) {
                    document.getElementById("txtYongliang").value = document.getElementById("bgzmy").value;
                }
                else {
                    document.getElementById("txtYongliang").value = document.getElementById("p_shuliang").value;
                 }
            }
            //型材
            else if (cailiaoname.indexOf("圆") > -1 || cailiaoname.indexOf("型钢") > -1 || cailiaoname == "扁钢" || cailiaoname.indexOf("焊管") > -1 || cailiaoname == "无缝钢管" || cailiaoname == "无缝管" || cailiaoname.indexOf("槽钢") > -1 || cailiaoname.indexOf("角钢") > -1 || cailiaoname == "工字钢" || cailiaoname == "方钢" || cailiaoname == "方钢管" || cailiaoname == "矩形管" || cailiaoname.indexOf("轨道") > -1 || mashape == "型" || mashape == "圆钢") {


                if (document.getElementById("ckbYL").checked == true) {
                    document.getElementById("cailiaodzh").value = (length * lilunzhl / 1000).toFixed(2);
                    document.getElementById("cailiaozongzhong").value = (length * lilunzhl * p_num * parseFloat(Xishu_X_Shape) / 1000).toFixed(2);
                    document.getElementById("cailiaozongchang").value = (length * p_num * parseFloat(Xishu_X_Shape)).toFixed(2);
                    document.getElementById("txtTuDz").value = document.getElementById("cailiaodzh").value;
                    document.getElementById("txtTuZz").value = (document.getElementById("txtTuDz").value * num).toFixed(2);
                }
                else {
                    document.getElementById("cailiaodzh").value = (length * lilunzhl / 1000).toFixed(2);
                    document.getElementById("cailiaozongzhong").value = (length * lilunzhl * p_num * 1 / 1000).toFixed(2);
                    document.getElementById("cailiaozongchang").value = (length * p_num * 1).toFixed(2);
                    document.getElementById("txtTuDz").value = document.getElementById("cailiaodzh").value;
                    document.getElementById("txtTuZz").value = (document.getElementById("txtTuDz").value * num).toFixed(2);
                }
            }
            else if (caigoudw.indexOf("(米") > -1 || caigoudw.indexOf("(m") > -1 || caigoudw.indexOf("(M") > -1 || caigoudw.indexOf("-米)") > -1 || caigoudw.indexOf("-m)") > -1 || caigoudw.indexOf("-M)") > -1) {
                document.getElementById("cailiaodzh").value = (lilunzhl * length / 1000).toFixed(3);
                document.getElementById("cailiaozongzhong").value = (p_num * lilunzhl * length / 1000).toFixed(2);
                document.getElementById("cailiaozongchang").value = p_num * length;
                document.getElementById("txtTuDz").value = document.getElementById("cailiaodzh").value;
                document.getElementById("txtTuZz").value = document.getElementById("cailiaozongzhong").value;
            }
            else {
                document.getElementById("cailiaodzh").value = (lilunzhl * 1).toFixed(3);
                document.getElementById("cailiaozongzhong").value = (p_num * lilunzhl).toFixed(2);
                document.getElementById("cailiaozongchang").value = "0";
                document.getElementById("txtTuDz").value = document.getElementById("cailiaodzh").value;
                document.getElementById("txtTuZz").value = document.getElementById("cailiaozongzhong").value;
            }
            if (document.getElementById("rblSFDC_1").checked) {
                document.getElementById("txtYongliang").value = document.getElementById("cailiaozongchang").value;
            }
            else {
                document.getElementById("txtYongliang").value = document.getElementById("p_shuliang").value;
            }
        }
        else if (xiaolei == "01.01") //标准件
        {
            if (caigoudw.indexOf("(米") > -1 || caigoudw.indexOf("(m") > -1 || caigoudw.indexOf("(M") > -1 || caigoudw.indexOf("-米)") > -1 || caigoudw.indexOf("-m)") > -1 || caigoudw.indexOf("-M)") > -1) {
                document.getElementById("cailiaodzh").value = (lilunzhl * length / 1000).toFixed(3);
                document.getElementById("cailiaozongzhong").value = (p_num * lilunzhl * length / 1000).toFixed(2);
                document.getElementById("cailiaozongchang").value = p_num * length;
                document.getElementById("txtTuDz").value = document.getElementById("cailiaodzh").value;
                document.getElementById("txtTuZz").value = document.getElementById("cailiaozongzhong").value;
            }
            else {
                document.getElementById("cailiaodzh").value = (lilunzhl * 1).toFixed(3);
                document.getElementById("cailiaozongzhong").value = (p_num * lilunzhl).toFixed(2);
                document.getElementById("cailiaozongchang").value = "0";
                document.getElementById("txtTuDz").value = document.getElementById("cailiaodzh").value;
                document.getElementById("txtTuZz").value = document.getElementById("cailiaozongzhong").value;
            }
            document.getElementById("txtYongliang").value = document.getElementById("p_shuliang").value;
        }
        //        AutoMarAreaByLenShape(length, mashape, sub_marid, document.getElementById("my"))
    }

}
//修改界面材料总重变化
function ChangeMarWeight(obj) {
    var pattem = /^\d+(\.\d+)?$/;
    var pnum = obj.value;
    var num = document.getElementById("hdcailiaozongzhong").value;
    if (!pattem.test(pnum)) {
        alert("请输入正确的数值！！！");
        obj.value = num;
        obj.focus();
        return false;
    }
    else {
        var maopi = document.getElementById("xinzhuang").value;
        if (maopi == "型" || maopi == "圆" ) {
            var unitweight = document.getElementById("lilunzhl").value;
            if (unitweight != 0) {
                document.getElementById("cailiaozongchang").value = ((pnum / unitweight) * 1000).toFixed(0);
            }
        }
    }


}
//计划数量改变
function ModifyChangePnum(obj) {
    var pattem = /^\d+(\.\d+)?$/;
    var pnum = obj.value;
    var num = document.getElementById("shuliang").value;
    if (!pattem.test(pnum)) {
        alert("请输入正确的数值！！！");
        obj.value = document.getElementById("shuliang").value;
        obj.focus();
        return false;
    }
    else if (parseFloat(pnum) < parseFloat(num)) {
        alert("计划数量不能小于总数量！！！");
        modifyCalculation(3);
    }
    else {
        var caigoudw = document.getElementById("techUnit").value;
        var maopi = document.getElementById("xinzhuang").value;
        var cailiaodz = document.getElementById("cailiaodzh").value;
        var cailiaocd = document.getElementById("cailiaocd").value;
        var cailiaozongzhong = document.getElementById("cailiaozongzhong").value;
        var cailiaozongchang = document.getElementById("cailiaozongchang").value;
        GetXiShu_TMSQL();
        var xishu = 1;
        if (maopi == "板") {
            xishu = parseFloat(Xishu_B_Shape);
        }
        else if (maopi == "型" || maopi == "圆") {
            xishu = parseFloat(Xishu_X_Shape);
        }

        if (!document.getElementById("ckbYL").checked) {
            xishu = 1;
        }

        if (maopi == "板") {
            cailiaozongzhong = (cailiaodz * pnum * xishu).toFixed(2);
        }
        else if (maopi == "型") {
            cailiaozongchang = (cailiaocd * pnum * xishu).toFixed(2);
            cailiaozongzhong = (cailiaodz * pnum * xishu).toFixed(2);
        }
        else if (caigoudw.indexOf("-(米") > -1 || caigoudw.indexOf("-(m") > -1 || caigoudw.indexOf("-(M") > -1 || caigoudw.indexOf("-米)") > -1 || caigoudw.indexOf("-m)") > -1 || caigoudw.indexOf("-M)") > -1) {
            cailiaozongchang = (cailiaocd * pnum * xishu).toFixed(2);
            cailiaozongzhong = (cailiaodz * pnum * xishu).toFixed(2);
        }
        else {
            cailiaozongzhong = (cailiaodz * pnum * xishu).toFixed(2);
        }
        document.getElementById("cailiaozongzhong").value = cailiaozongzhong;
        document.getElementById("cailiaozongchang").value = cailiaozongchang;
    }
}

function Calculation(i) {
    var text = i;
    var pattem = /^\d+(\.\d+)?$/;
    var num = document.getElementById("shuliang").value;
    var cailiaodzh = document.getElementById("cailiaodzh").value;
    var dzh = document.getElementById("txtTuDz").value;
    var bgzmy = document.getElementById("my").value;
    var cailiaoguige = document.getElementById("cailiaoguige").value;
    var lilunzhl = document.getElementById("lilunzhl").value;
    var cailiaoname = document.getElementById("cnname").value;
    var maopi = document.getElementById("cailiaoType").value; //毛坯
    var unit = document.getElementById("techUnit").value; //单位
    var cailiaozhongchang = document.getElementById("cailiaozongchang").value;
    var marid = document.getElementById("marid").value;
    GetXiShu_TMSQL();
    if (marid != "" && maopi == "") {
        alert("请输入材料种类！！！");
        return;
    }
    switch (text) {
        case 1: //材料单重改变
            if (marid == "") {
                alert("非物料，输入材料单重无效！！！");
                document.getElementById("cailiaodzh").value = document.getElementById("hdcailiaodzh").value;
                return;
            }
            if (pattem.test(cailiaodzh)) {
                if (maopi == "板" || cailiaoname.indexOf("钢板") > -1 || cailiaoname.indexOf("花纹板") > -1 || cailiaoname.indexOf("钢格板") > -1 || cailiaoname.indexOf("栅格板") > -1 || cailiaoname.indexOf("钢板网") > -1)//
                {
                    if (cailiaoname.indexOf("花纹板") > -1 || cailiaoname.indexOf("钢格板") > -1 || cailiaoname.indexOf("栅格板") > -1 || cailiaoname.indexOf("钢板网") > -1) {
                        Xishu_B_Shape = "1";
                        document.getElementById("cailiaozongzhong").value = (cailiaodzh * num * parseFloat(Xishu_B_Shape)).toFixed(2);
                    }
                    else {
                        document.getElementById("cailiaozongzhong").value = (cailiaodzh * num * parseFloat(Xishu_B_Shape)).toFixed(2);
                    }
                }
                else if (maopi == "型" || maopi == "圆") {
                    document.getElementById("cailiaozongzhong").value = (cailiaodzh * num * parseFloat(Xishu_X_Shape)).toFixed(2);
                }
                else {
                    document.getElementById("cailiaozongzhong").value = (cailiaodzh * num).toFixed(2);
                }
            }
            else {
                alert('输入格式有误!');
            }
            break;
        case 2: //单重改变
            if (pattem.test(dzh)) {
                document.getElementById("zongzhong").value = (dzh * num).toFixed(2);
                if (maopi == "板" && cailiaoname != "钢板网" && cailiaoname != "花纹板" && cailiaoname != "栅格板") {
                    document.getElementById("my").value = (dzh / (lilunzhl * cailiaoguige)).toFixed(2);
                }
                else if (cailiaoname.indexOf("花纹板") > -1 || cailiaoname.indexOf("钢格板") > -1 || cailiaoname.indexOf("栅格板") > -1 || cailiaoname.indexOf("钢板网") > -1) {
                    document.getElementById("my").value = (dzh / lilunzhl).toFixed(2);
                }
                document.getElementById("txtTuZz").value = (dzh * num).toFixed(2);
            }
            else {
                alert('输入格式有误!');
            }
            break;
        case 3: //面域改变
            if (pattem.test(bgzmy)) {
                if (marid != "") {
                    if (maopi == "板" && cailiaoname != "钢板网" && cailiaoname != "花纹板" && cailiaoname != "栅格板") {
                        if (document.getElementById("cailiaodzh").value == "" || document.getElementById("cailiaodzh").value == "0") {
                            document.getElementById("cailiaodzh").value = (bgzmy * cailiaoguige * lilunzhl).toFixed(2);
                            document.getElementById("cailiaozongzhong").value = (bgzmy * cailiaoguige * lilunzhl * parseFloat(Xishu_B_Shape)).toFixed(2);
                        }
                        document.getElementById("txtTuDz").value = (bgzmy * cailiaoguige * lilunzhl).toFixed(2);
                        document.getElementById("txtTuZz").value = (bgzmy * cailiaoguige * lilunzhl * num).toFixed(2);
                    }
                    else if (cailiaoname.indexOf("花纹板") > -1 || cailiaoname.indexOf("钢格板") > -1 || cailiaoname.indexOf("栅格板") > -1 || cailiaoname.indexOf("钢板网") > -1) {
                        Xishu_B_Shape = "1";
                        if (document.getElementById("cailiaodzh").value == "" || document.getElementById("cailiaodzh").value == "0") {
                            document.getElementById("cailiaodzh").value = (bgzmy * lilunzhl).toFixed(2);
                            document.getElementById("cailiaozongzhong").value = (bgzmy * lilunzhl * parseFloat(Xishu_B_Shape)).toFixed(2);
                        }
                        document.getElementById("txtTuDz").value = (bgzmy * lilunzhl).toFixed(2);
                        document.getElementById("txtTuZz").value = (bgzmy * lilunzhl * num).toFixed(2);
                    }
                    if (document.getElementById("rblSFDC_1").checked) {
                        document.getElementById("txtYongliang").value = document.getElementById("bgzmy").value;
                    }
                    else {
                        document.getElementById("txtYongliang").value = document.getElementById("p_shuliang").value;
                    }
                }
            }
            else {
                alert('输入格式有误!');
            }
            break;
        case 4: //材料总长改变
            if (pattem.test(cailiaozhongchang)) {
                if (maopi == "板") {
                    alert("毛坯形状为【板】,输入【材料总长】无效！！！");
                }
                else if (maopi == "型" || maopi == "圆" || unit.indexOf("(米-") > -1 || unit.indexOf("-米)")) {
                if (document.getElementById("rblSFDC_1").checked) {
                    document.getElementById("txtYongliang").value = document.getElementById("cailiaozongchang").value;
                }
                else {
                    document.getElementById("txtYongliang").value = document.getElementById("p_shuliang").value;
                }
                }
            }
            else {
                alert('输入格式有误!');
                document.getElementById("cailiaozongchang").value = document.getElementById("hdcailiaozongchang").value;
            }
            break;
        default:
            break;
    }
}











function GetFjMsXuHao(tablename, tsaid, _fjxuhao) {
    conn = new ActiveXObject("adodb.connection")
    connstr = GetConnection();
    conn.open(connstr);
    if (conn.State == 1) {
        comText = "select BM_MSXUHAO from " + tablename + " where BM_ENGID='" + tsaid + "' and BM_XUHAO='" + _fjxuhao + "' ";
        objrs = conn.Execute(comText);
    }
    if (!objrs.BOF & !objrs.EOF) {
        return objrs.Fields(0).Value;
    }
    else {
        return "";
    }
}


function GetXiShu_TMSQL() {
    if (document.getElementById("rblSFDC_0").checked) {
        Xishu_B_Shape = "1";
        Xishu_X_Shape = "1";
    }
    else {
        Xishu_B_Shape = document.getElementById("txtBxishu").value;
        Xishu_X_Shape = document.getElementById("txtXxishu").value;
    }
}
//查看》修改原数据界面》是否定尺改变
function ModifyFix(obj) {
    var marid = document.getElementById("marid").value;
    var dw = document.getElementById("techUnit").value; //技术单位
    var caigoudw = dw;
    var bancai = document.getElementById("cailiaoType").value;
    var clcd = document.getElementById("cailiaocd").value;
    var cldzh = document.getElementById("cailiaodzh").value;
    var shuliang = document.getElementById("p_shuliang").value;
    var my=document.getElementById("my").value;
    if (marid != "") {
        if (document.getElementById("rblSFDC_0").checked)//定尺
        {
            //材料总重
            document.getElementById("cailiaozongzhong").value = (cldzh * shuliang).toFixed(2);
            document.getElementById("bgzmy").value = (my * shuliang).toFixed(2);
            if (bancai == "型" || bancai == "圆" || caigoudw.indexOf("(米") > -1 || caigoudw.indexOf("(m") > -1 || caigoudw.indexOf("(M") > -1 || caigoudw.indexOf("-米)") > -1 || caigoudw.indexOf("-m)") > -1 || caigoudw.indexOf("-M)") > -1) {
                //材料总长
                document.getElementById("cailiaozongchang").value = (clcd * shuliang).toFixed(2);
            }
        }
        else {
            GetXiShu_TMSQL();
            if (bancai == "型" || bancai == "圆" || caigoudw.indexOf("(米") > -1 || caigoudw.indexOf("(m") > -1 || caigoudw.indexOf("(M") > -1 || caigoudw.indexOf("-米)") > -1 || caigoudw.indexOf("-m)") > -1 || caigoudw.indexOf("-M)") > -1) {
                //材料总重
                document.getElementById("cailiaozongzhong").value = (cldzh * shuliang * parseFloat(Xishu_X_Shape)).toFixed(2);

                //材料总长
                document.getElementById("cailiaozongchang").value = (clcd * shuliang * parseFloat(Xishu_X_Shape)).toFixed(2);
               
            }
            else if (bancai == "板") {
                //材料总重
            document.getElementById("cailiaozongzhong").value = (cldzh * shuliang * parseFloat(Xishu_B_Shape)).toFixed(2);
            document.getElementById("bgzmy").value = (my * shuliang * parseFloat(Xishu_B_Shape)).toFixed(2);
            }
            else {
                //材料总重
                document.getElementById("cailiaozongzhong").value = (cldzh * shuliang).toFixed(2);
            }
        }
    }
}

//根据采购主单位验证重量、长度、数量等不能为空
function CheckNumNotZeroWithPurUnit(type) {
    var _tablename;
    var _tablerows
    var _marid;
    var _dw;
    var _p_number;
    var _cailiaozhongchang;
    var _cailiaozhongzhong;
    var _mpmy;

    switch (type) {
        case 0: //BOM录入界面
            _tablename = document.getElementById(getClientId().Id1);
            _tablerows = _tablename.rows.length;
            for (var i = 1; i < _tablerows; i++) {
                _marid = _tablename.rows[i].cells[3].getElementsByTagName("input")[0].value;
                if (_marid != "") {
                    _dw = _tablename.rows[i].cells[22].getElementsByTagName("input")[0].value
                    //采购主单位为长度单位：米
                    if (_dw.indexOf("(米-") > -1 || _dw.indexOf("(M-") > -1 || _dw.indexOf("(m-") > -1) {
                        _cailiaozhongchang = _tablename.rows[i].cells[31].getElementsByTagName("input")[0].value;
                        if (_cailiaozhongchang == "" || parseFloat(_cailiaozhongchang) <= 0) {
                            alert("第" + i + "行:采购主单位为【米】,必须输入【材料长度】及【材料总长】！！！");
                            return false;
                        }
                    }
                    //采购主单位为重量单位：kg 、T
                    else if (_dw.indexOf("(T-") > -1 || _dw.indexOf("(t-") > -1 || _dw.indexOf("(吨-") > -1 || _dw.indexOf("(kg-") > -1 || _dw.indexOf("(KG-") > -1 || _dw.indexOf("(千克-") > -1 || _dw.indexOf("(Kg-") > -1 || _dw.indexOf("(kG-") > -1 || _dw.indexOf("(公斤-") > -1) {
                        _cailiaozhongzhong = _tablename.rows[i].cells[17].getElementsByTagName("input")[0].value;
                        if (_cailiaozhongzhong == "" || parseFloat(_cailiaozhongzhong) <= 0) {
                            alert("第" + i + "行:采购主单位为重量单位,必须输入【材料单重】及【材料总重】！！！");
                            return false;
                        }
                    }
                    else if (_dw.indexOf("(平方米-") > -1 || _dw.indexOf("(平米-") > -1 || _dw.indexOf("(m2-") > -1 || _dw.indexOf("(M2-") > -1 || _dw.indexOf("(㎡-") > -1) {
                        _mpmy = _tablename.rows[i].cells[19].getElementsByTagName("input")[1].value;
                        if (_mpmy == "" || parseFloat(_mpmy) <= 0) {
                            alert("第" + i + "行:采购主单位为面积单位,必须输入【计划面域】！！！");
                            return false;
                        }
                    }
                    //采购单位为其它单位
                    else {
                        _p_number = _tablename.rows[i].cells[11].getElementsByTagName("input")[0].value;
                        if (_p_number == "" || parseFloat(_p_number) <= 0) {
                            alert("第" + i + "行:请输入数量！！！");
                            return false;
                        }
                    }
                }
            }
            break;

        case 1: //批量复制及修改界面
            _tablename = document.getElementById(getClientId().Id1);
            _tablerows = _tablename.rows.length;
            for (var i = 1; i < _tablerows; i++) {
                _marid = _tablename.rows[i].cells[3].getElementsByTagName("input")[0].value;
                if (_marid != "") {
                    _dw = _tablename.rows[i].cells[25].getElementsByTagName("input")[0].value
                    //采购主单位为长度单位：米
                    if (_dw.indexOf("(米-") > -1 || _dw.indexOf("(M-") > -1 || _dw.indexOf("(m-") > -1) {
                        _cailiaozhongchang = _tablename.rows[i].cells[24].getElementsByTagName("input")[0].value;
                        if (_cailiaozhongchang == "" || parseFloat(_cailiaozhongchang) <= 0) {
                            alert("第" + i + "行:采购主单位为【米】,必须输入【材料长度】及【材料总长】！！！");
                            return false;
                        }
                    }
                    //采购主单位为重量单位：kg 、T
                    else if (_dw.indexOf("(T-") > -1 || _dw.indexOf("(t-") > -1 || _dw.indexOf("(吨-") > -1 || _dw.indexOf("(kg-") > -1 || _dw.indexOf("(KG-") > -1 || _dw.indexOf("(千克-") > -1 || _dw.indexOf("(Kg-") > -1 || _dw.indexOf("(kG-") > -1 || _dw.indexOf("(公斤-") > -1) {
                        _cailiaozhongzhong = _tablename.rows[i].cells[12].getElementsByTagName("input")[0].value;
                        if (_cailiaozhongzhong == "" || parseFloat(_cailiaozhongzhong) <= 0) {
                            alert("第" + i + "行:采购主单位为重量单位,必须输入【材料单重】及【材料总重】！！！");
                            return false;
                        }
                    }
                    //采购单位为平米
                    else if (_dw.indexOf("(平方米-") > -1 || _dw.indexOf("(平米-") > -1 || _dw.indexOf("(m2-") > -1 || _dw.indexOf("(M2-") > -1 || _dw.indexOf("(㎡-") > -1) {
                        _mpmy = _tablename.rows[i].cells[13].getElementsByTagName("input")[1].value;
                        if (_mpmy == "" || parseFloat(_mpmy) <= 0) {
                            alert("第" + i + "行:采购主单位为面积单位,必须输入【计划面域】！！！");
                            return false;
                        }
                    }
                    //采购单位为其它单位
                    else {
                        _p_number = _tablename.rows[i].cells[9].getElementsByTagName("input")[0].value;
                        if (_p_number == "" || parseFloat(_p_number) <= 0) {
                            alert("第" + i + "行:请输入数量！！！");
                            return false;
                        }
                    }
                }
            }
            break;

        case 2: //弹出框修改界面
            _marid = document.getElementById("marid").value;
           
            if (_marid != "") {
                _dw = document.getElementById("techUnit").value;
                //采购主单位为长度单位：米
                if (_dw.indexOf("(米-") > -1 || _dw.indexOf("(M-") > -1 || _dw.indexOf("(m-") > -1) {
                    _cailiaozhongchang = document.getElementById("cailiaozongchang").value;
                    if (_cailiaozhongchang == "" || parseFloat(_cailiaozhongchang) <= 0) {
                        alert("采购主单位为【米】,必须输入【材料长度】及【材料总长】！！！");
                        return false;
                    }
                }
                //采购主单位为重量单位：kg 、T
                else if (_dw.indexOf("(T-") > -1 || _dw.indexOf("(t-") > -1 || _dw.indexOf("(吨-") > -1 || _dw.indexOf("(kg-") > -1 || _dw.indexOf("(KG-") > -1 || _dw.indexOf("(千克-") > -1 || _dw.indexOf("(Kg-") > -1 || _dw.indexOf("(kG-") > -1 || _dw.indexOf("(公斤-") > -1) {
                    _cailiaozhongzhong = document.getElementById("cailiaozongzhong").value;
                    if (_cailiaozhongzhong == "" || parseFloat(_cailiaozhongzhong) <= 0) {
                        alert("采购主单位为重量单位,必须输入【材料单重】及【材料总重】！！！");
                        return false;
                    }
                }
                //采购单位为平米
                else if (_dw.indexOf("(平米-") > -1 || _dw.indexOf("(平方米-") > -1 || _dw.indexOf("(M2-") > -1 || _dw.indexOf("(m2-") > -1 || _dw.indexOf("(㎡-") > -1) {
                    _mpmy = document.getElementById("bgzmy").value;
                    if (_mpmy == "" || parseFloat(_mpmy) <= 0) {
                        alert("采购主单位为面积单位,必须输入【计划面域】！！！");
                        return false;
                    }
                }
                //采购单位为其它单位
                else {
                    _p_number = document.getElementById("p_shuliang").value;
                    if (_p_number == "" || parseFloat(_p_number) <= 0) {
                        alert("请输入数量！！！");
                        return false;
                    }
                }
            }
            break;




        default:
            break;
    }
  
    return true;
}



////根据物料编码的长度，毛坯形状，物料编码返回面域
//function AutoMarAreaByLenShape(_len, _marshape, _marid, obj) {
//    if (_marshape == "型" || _marshape == "圆") {
//        connSql(5);
//        var unitmy = objrs.Fields(0).Value;
//        var _my = parseFloat(unitmy) * _len / 1000;
//        if (_my > 0.001) {
//            obj.value = parseFloat(_my).toFixed(3);
//        }
//        else {
//            obj.value = parseFloat(_my).toFixed(4);
//        }
//    }
//}