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
    //    var rngTxt = input.createTextRange(); //建立文本域
    //    var m = (rngTxt.text).length;
    var rngTxt = input.value; //建立文本域
    var m = rngTxt.length;
    var cellIndex = GetRealCellIndex(input);
    /////alert(cellIndex);
    var key = window.event.keyCode;   //获得按钮的编号


    if (key == 37)   //向左
    {
        var n = getCursorPos(input);
        if (n == 0) {
            //是否为第一列
            for (var i = cellIndex - 1; i > 0; i--) {
                //alert(i);
                if (tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0] == null || tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].readOnly || tr[rowIndex].getElementsByTagName("td")[i].className == "hidden") {
                    continue;
                }
                else {
                    tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].focus();
                    tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].select();
                    break;
                }
            }
        }
    }

    if (key == 38)  //向上
    {
        for (var i = rowIndex - 1; i > 0; i--) {
            if (tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0] != null || tr[i].getElementsByTagName("td")[cellIndex].className == 'hidden') {
                tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].focus();
                tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].select();
                break;
            }
            else {
                continue;
            }
        }
    }

    if (key == 39)  //向右
    {
        var n = getCursorPos(input);
        if (n == m) {
            for (var i = cellIndex + 1; i < cellcount; i++) {
                if (i < cellcount) {
                    if (tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0] == null || tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].readOnly || tr[rowIndex].getElementsByTagName("td")[i].className == 'hidden')//
                    {
                        continue;
                    }
                    else {
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
    }

    if (key == 40)   //向下
    {
        for (var i = rowIndex + 1; i < rowcount; i++) {
            if (tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0] != null || tr[i].getElementsByTagName("td")[cellIndex].className == 'hidden') {
                tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].focus();
                tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].select();
                break;
            }
            else {
                continue;
            }
        }
    }
}

function GetRealCellIndex(input) {
    return $(input).parent().parent().find("input").index(input);
}

/*获取光标在文本中的位置*/
function getCursorPos(obj) {
    //    var rngSel = document.selection.createRange(); //建立选择域
    //    var rngTxt = obj.createTextRange(); //建立文本域
    //    var flag = rngSel.getBookmark(); //用选择域建立书签
    //    rngTxt.collapse(); //瓦解文本域到开始位,以便使标志位移动
    //    rngTxt.moveToBookmark(flag); //使文本域移动到书签位
    //    rngTxt.moveStart('character', -obj.value.length); //获得文本域左侧文本
    //    str = rngTxt.text.replace(/\r\n/g, ''); //替换回车换行符
    //    return (str.length); //返回文本域文本长度
    var result = 0;
    if (typeof (obj.selectionStart) != "undefined") { //IE
        result = obj.selectionStart
    } else { //非IE 
        var rng;
        if (obj.tagName == "textarea") { //TEXTAREA 
            rng = event.srcElement.createTextRange();
            rng.moveToPoint(event.x, event.y);
        } else { //Text 
            rng = document.selection.createRange();
        }
        rng.moveStart("character", -event.srcElement.value.length);
        result = rng.text.length;
    }
    return result;
}
