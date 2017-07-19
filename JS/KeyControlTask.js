
//处理键盘事件 禁止后退键（Backspace）密码或单行、多行文本框除外和F5刷新
function keypresscheck(e) {
    var code;
    if (!e) var e = window.event;
    if (e.keyCode) code = e.keyCode;
    else if (e.which) code = e.which;
    if (((event.keyCode == 8) &&                                                    //BackSpace   
         ((event.srcElement.type != "text" &&
         event.srcElement.type != "textarea" &&
         event.srcElement.type != "password") ||
         event.srcElement.readOnly == true)) ||
        ((event.ctrlKey) && ((event.keyCode == 78) || (event.keyCode == 82))) ||    //CtrlN,CtrlR   
        (event.keyCode == 116)) {                                                   //F5   
        event.keyCode = 0;
        event.returnValue = false;
    }
    return true;
}
//Table的←↑→↓控制
function grControlFocus(input) {
    var e = event.srcElement;
    var rowIndex = e.parentNode.parentNode.rowIndex; //获取行号
    var tr = e.parentNode.parentNode.parentNode.getElementsByTagName("tr"); //获取行
    var rowcount = tr.length - 1;  //行数
    var td = e.parentNode.parentNode.getElementsByTagName("td"); //获取行的单元格
    var cellcount = td.length - 1;   //列数
    var key = window.event.keyCode;   //获得按钮的编号
    var cellIndex = e.parentNode.cellIndex;  //获取焦点的列号
    var rngTxt = input.createTextRange(); //建立文本域
    var m = (rngTxt.text).length;
    if (key == 37)   //向左 
    {
        var n = getCursorPos(input);
        if (n == 0) {
            var nextid;
            if (rowIndex == 1)  //第一行
            {
                if (cellIndex != 3) {
                    nextid = tr[rowIndex].getElementsByTagName("td")[cellIndex - 1].getElementsByTagName("textarea")[0];
                    nextid.focus();
                    getSelectPos(nextid);
                }
                else   //第一行第二列
                {
                    tr[rowIndex].getElementsByTagName("td")[cellIndex].getElementsByTagName("textarea")[0].focus();
                }
            }
            else {
                if (cellIndex != 3) {
                    nextid = tr[rowIndex].getElementsByTagName("td")[cellIndex - 1].getElementsByTagName("textarea")[0];
                    nextid.focus();
                    getSelectPos(nextid);
                }
                else {
                    nextid = tr[rowIndex - 1].getElementsByTagName("td")[cellcount].getElementsByTagName("textarea")[0];
                    nextid.focus();
                    getSelectPos(nextid);
                }
            }
        }
    }
    if (key == 38)  //向上
    {
        if (rowIndex != 1) {
            tr[rowIndex - 1].getElementsByTagName("td")[cellIndex].getElementsByTagName("textarea")[0].focus();
        }
        else {
            tr[rowIndex].getElementsByTagName("td")[cellIndex].getElementsByTagName("textarea")[0].focus();
        }
    }
    if (key == 39)  //向右
    {
        var n = getCursorPos(input);
        if (n == m) {
            if (rowIndex != rowcount) {
                if (cellIndex != cellcount) {
                    tr[rowIndex].getElementsByTagName("td")[cellIndex + 1].getElementsByTagName("textarea")[0].focus();
                }
                else {
                    tr[rowIndex + 1].getElementsByTagName("td")[2].getElementsByTagName("textarea")[0].focus();
                }
            }
            else {
                if (cellIndex != cellcount) {
                    tr[rowIndex].getElementsByTagName("td")[cellIndex + 1].getElementsByTagName("textarea")[0].focus();
                }
                else {
                    tr[rowIndex].getElementsByTagName("td")[cellIndex].getElementsByTagName("textarea")[0].focus();
                }
            }
        }

    }
    if (key == 40)   //向下
    {
        if (rowIndex != rowcount) {
            tr[rowIndex + 1].getElementsByTagName("td")[cellIndex].getElementsByTagName("textarea")[0].focus();
        }
        else {
            tr[rowIndex].getElementsByTagName("td")[cellIndex].getElementsByTagName("textarea")[0].focus();
        }
    }
}



function getSelectPos(obj) {
    var rtextRange = obj.createTextRange(); //建立文本域
    rtextRange.moveStart('character', obj.value.length); //光标的位置
    rtextRange.collapse(true);
    rtextRange.select();
}


/*获取光标在文本中的位置*/
function getCursorPos(obj) {
    var rngSel = document.selection.createRange(); //建立选择域
    var rngTxt = obj.createTextRange(); //建立文本域
    var flag = rngSel.getBookmark(); //用选择域建立书签
    rngTxt.collapse(); //瓦解文本域到开始位,以便使标志位移动
    rngTxt.moveToBookmark(flag); //使文本域移动到书签位
    rngTxt.moveStart('character', -obj.value.length); //获得文本域左侧文本
    str = rngTxt.text.replace(/\r\n/g, ''); //替换回车换行符
    return (str.length); //返回文本域文本长度
}


function MouseClick(obj) {
    var table = obj.parentNode.parentNode;
    var tr = table.getElementsByTagName("tr");
    var ss = tr.length;
    for (var i = 4; i < ss - 2; i++) {
        tr[i].style.backgroundColor = (tr[i].style.backgroundColor == '#FFD700') ? '#ffffff' : '#ffffff';
    }
    obj.style.backgroundColor = (obj.style.backgroundColor == '#ffffff') ? '#FFD700' : '#ffffff';
}

function MouseClick1(obj) {
    var table = obj.parentNode.parentNode;
    var tr = table.getElementsByTagName("tr");
    var ss = tr.length;
    for (var i = 1; i < ss - 1; i++) {
        tr[i].style.backgroundColor = (tr[i].style.backgroundColor == '#FFD700') ? '#ffffff' : '#ffffff';
    }
    obj.style.backgroundColor = (obj.style.backgroundColor == '#ffffff') ? '#FFD700' : '#ffffff';
}



//Table的←↑→↓控制
function grControlFocus1(input) {
    var e = event.srcElement;
    var rowIndex = e.parentNode.parentNode.rowIndex; //获取行号
    var tr = e.parentNode.parentNode.parentNode.getElementsByTagName("tr"); //获取行
    var rowcount = tr.length - 1;  //行数
    var td = e.parentNode.parentNode.getElementsByTagName("td"); //获取行的单元格
    var cellcount = td.length - 3;   //列数
    var key = window.event.keyCode;   //获得按钮的编号
    var cellIndex = e.parentNode.cellIndex;  //获取焦点的列号
    var rngTxt = input.createTextRange(); //建立文本域
    var m = (rngTxt.text).length;

    if (key == 38)  //向上
    {
        if (rowIndex != 1) {
            tr[rowIndex - 1].getElementsByTagName("td")[cellIndex].getElementsByTagName("textarea")[0].focus();
        }
        else {
            tr[rowIndex].getElementsByTagName("td")[cellIndex].getElementsByTagName("textarea")[0].focus();
        }
    }

    if (key == 40)   //向下
    {
        if (rowIndex != rowcount) {
            tr[rowIndex + 1].getElementsByTagName("td")[cellIndex].getElementsByTagName("textarea")[0].focus();
        }
        else {
            tr[rowIndex].getElementsByTagName("td")[cellIndex].getElementsByTagName("textarea")[0].focus();
        }
    }
}