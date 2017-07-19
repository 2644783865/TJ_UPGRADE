<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="QC_Inspection_Add.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_Inspection_Add"
    Title="质量报检管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="cache-control" content="no-cache">
    <meta http-equiv="expires" content="0">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../SM_Data/javascripts/superTables.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .fakeContainer
        {
            /* The parent container */
            width: 100%; /* Required to set */
            height: 350px; /* Required to set */
            overflow: hidden; /* Required to set */
        }
    </style>

    <script src="../SM_Data/javascripts/superTables.js" type="text/javascript"></script>

    <script src="../SM_Data/javascripts/jquery.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        window.onload = function() {
            document.getElementById("table_1").style.width = window.screen.availWidth - 50;
        }
        function Getjhstate(obj, n) {

            var text = obj.value;
            var index = obj.parentNode.parentNode.parentNode.rowIndex;
            var tr = $("div.sData>table")[0].tBodies[0].rows;
            if (index == 2 && text != "") {
                for (i = 3; i < tr.length - 1; i++) {
                    var jhstate = tr[i].getElementsByTagName("td")[n].getElementsByTagName("input")[0];
                    if (jhstate.value == "") {
                        jhstate.value = text;
                    }
                }
            }
        }

        function tx() {
            var partname = document.getElementById('<%=TextBoxPartName.ClientID%>').value;
            var site = document.getElementById('<%=TextBoxSite.ClientID%>').value;
            var contract = document.getElementById('<%=TextBoxContracter.ClientID%>').value;
            var tel = document.getElementById('<%=TextBoxTel.ClientID%>').value;
            var date = document.getElementById('<%=TextBoxDate.ClientID%>').value;
            if (partname == "" || site == "" || contract == "" || tel == "" || date == "") {
                if (partname == "") {
                    document.getElementById('<%=Labelpartnm.ClientID%>').style.display = "block";
                }
                if (site == "") {
                    document.getElementById('<%=Labelsit.ClientID%>').style.display = "block";
                }
                if (contract == "") {
                    document.getElementById('<%=LabelContracter.ClientID%>').style.display = "block";
                }
                if (tel == "") {
                    document.getElementById('<%=LabelTel.ClientID%>').style.display = "block";
                }
                if (date == "") {
                    document.getElementById('<%=LabelDate.ClientID%>').style.display = "block";
                }
                alert("*为必填项");
                return false;

            }
            var tr = $("div.sFData>div.sFDataInner>table")[0].tBodies[0].rows;
            var num = "";
            for (i = 2; i < tr.length - 1; i++) {
                label = tr[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].innerHTML;

                if (label == "已报检") {
                    num += (i - 1) + ",";
                }
            }

            num = num.substring(0, num.length - 1);

            if (num != "") {
                var ischeck = confirm("第【" + num + "】条计划跟踪号已经报过检了，需要再次报检吗？");
                return ischeck;
            }

            var btntext = document.getElementById('<%=Submit.ClientID%>').value;
            if (btntext == "修改质检") {
                var n = 0;
                for (i = 2; i < tr.length - 1; i++) {
                    var check = tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked;
                    if (check == true) {
                        n++;
                    }
                }
                if (n == 0) {
                    var ischeck = confirm("未勾选质检明细，确认要修改质检吗？");
                    return ischeck;
                }

            }
        }

        $(function() {
            $("#table1 tr").click(function() {
                //   console.log($(this).attr("class"));
                $(this).removeAttr("style");

                $(this).toggleClass("techBackColor");
            });
        });

        $(function() {
            $("#table1 tr:eq(2) input[name*=TextBoxQCMan]").keyup(function() {
                $("#table1 input[name*=TextBoxQCMan]").val($(this).val());
            });
            $("#table1 tr:eq(2) input[name*=TextBoxState]").keyup(function() {
            $("#table1 input[name*=TextBoxState]").val($(this).val());
            });
            $("#table1 tr:eq(2) input[name*=TextBoxDate]").change(function() {
            $("#table1 input[name*=TextBoxDate]").val($(this).val());
            });
        });
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="Submit" runat="server" Text="提交" OnCommand="Submit_Command" OnClientClick="return tx();" />&nbsp;
                            &nbsp;
                            <%--<input id="Cancel" type="button" value="取消" onclick="return history.go(-1)" />&nbsp;--%>
                            <asp:Button ID="Back" runat="server" Text="返回" OnClick="Back_Click" CausesValidation="false" />
                            &nbsp;
                            <asp:Button ID="Export" runat="server" Text="导出" OnClick="btn_export_Click" CausesValidation="false" />&nbsp;
                            &nbsp;
                            <asp:Label ID="LabelNotes" runat="server" Text="修改质检和重新质检时请勾选需要质检的子项" Visible="false"
                                Style="color: #FF3300"></asp:Label>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfduniqueid" runat="server" />
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="width: 100%">
                <table cellpadding="4" style="margin-right: auto; margin-left: auto">
                    <tr>
                        <td align="center" colspan="9">
                            <div align="center">
                                <h2>
                                    <strong>报检单</strong></h2>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            项目名称:
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelProName" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center">
                            设备名称:
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelEngName" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center">
                            报检部门:
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelDep" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center">
                            报检人:
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelMan" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="LabelManName" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="left">
                            任务号:
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelEngID" runat="server" Text="" Width="100px"></asp:Label>
                            <asp:Label ID="LabelState" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            部件名称:
                        </td>
                        <td align="left" style="white-space: nowrap">
                            <asp:TextBox ID="TextBoxPartName" runat="server" Style="float: left"></asp:TextBox>
                            <asp:Label ID="Labelpartnm" runat="server" Text="*" ForeColor="Red" Width="2px" Style="display: none;
                                float: right"></asp:Label>
                        </td>
                        <td align="center">
                            供货单位:
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelSupplier" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center">
                            检查地点:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TextBoxSite" runat="server" Style="float: left"></asp:TextBox>
                            <asp:Label ID="Labelsit" runat="server" Text="*" ForeColor="Red" Width="2px" Style="display: none;
                                float: right"></asp:Label>
                        </td>
                        <td align="center">
                            报检时间:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TextBoxData" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="TextBoxBJData" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:Label ID="LabelZJBH" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            联系人:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TextBoxContracter" runat="server" Style="float: left">
                            </asp:TextBox>
                            <asp:Label ID="LabelContracter" runat="server" Text="*" ForeColor="Red" Width="2px"
                                Style="display: none; float: right"></asp:Label>
                        </td>
                        <td align="center">
                            联系电话:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TextBoxTel" runat="server" Style="float: left"></asp:TextBox>
                            <asp:Label ID="LabelTel" runat="server" Text="*" ForeColor="Red" Width="2px" Style="display: none;
                                float: right"></asp:Label>
                        </td>
                        <td align="center">
                            需要质检时间:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TextBoxDate" runat="server" Style="float: left"></asp:TextBox><asp:Label
                                ID="LabelDate" runat="server" Text="*" ForeColor="Red" Width="2px" Style="display: none;
                                float: right"></asp:Label>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxDate"
                                TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd" FirstDayOfWeek="Monday">
                            </asp:CalendarExtender>
                        </td>
                        <td align="center">
                            备注:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TextBoxNote" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <div class="fakeContainer" id="div" style="width: 100%; overflow: auto">
                <asp:HiddenField ID="hfdshuliang" runat="server" />
                <asp:HiddenField ID="hfddanzhong" runat="server" />
                <asp:HiddenField ID="hfdzongzhong" runat="server" />
                <asp:Repeater ID="RepeaterItem" runat="server" OnItemDataBound="RepeaterItem_ItemDataBound">
                    <HeaderTemplate>
                        <table id="table1" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr class="tableTitle1">
                                <td rowspan="2">
                                    <div align="center">
                                        序号</div>
                                </td>
                                <td rowspan="2">
                                    <div align="center">
                                        计划跟踪号</div>
                                </td>
                                <td rowspan="2">
                                    <div align="center">
                                        子项名称</div>
                                </td>
                                <td rowspan="2">
                                    <div align="center">
                                        图号/标识符</div>
                                </td>
                                <td rowspan="2">
                                    <div align="center">
                                        物料编码</div>
                                </td>
                                <td rowspan="2">
                                    <div align="center">
                                        规格
                                    </div>
                                </td>
                                <td rowspan="2">
                                    <div align="center">
                                        材质
                                    </div>
                                </td>
                                <td rowspan="2">
                                    <div align="center">
                                        国标
                                    </div>
                                </td>
                                <td rowspan="2">
                                    <div align="center">
                                        数量</div>
                                </td>
                                <%--    <td colspan="2" visible="false">
                                    <div align="center">
                                        重量(T)</div>
                                </td>--%>
                                <td colspan="3">
                                    <div align="center">
                                        报检数量</div>
                                </td>
                                <td rowspan="2">
                                    <div align="center">
                                        报检时间</div>
                                </td>
                                <td rowspan="2">
                                    <div align="center">
                                        交货状态</div>
                                </td>
                                <td colspan="8">
                                    <div align="center">
                                        检查结果</div>
                                </td>
                                <td rowspan="2">
                                    <div align="center">
                                        材料类别</div>
                                </td>
                                <td rowspan="2">
                                    <div align="center">
                                        是否定尺</div>
                                </td>
                                <td rowspan="2">
                                    <div align="center">
                                        长度
                                    </div>
                                </td>
                                <td rowspan="2">
                                    <div align="center">
                                        宽度
                                    </div>
                                </td>
                                <td rowspan="2">
                                    <div align="center">
                                        单位</div>
                                </td>
                            </tr>
                            <tr class="tableTitle1">
                                <%--<td>
                                    <div align="center"  visible="false">
                                        单重</div>
                                </td>
                                <td>
                                    <div align="center"  visible="false">
                                        总重</div>
                                </td>--%>
                                <td>
                                    <div align="center">
                                        数量</div>
                                </td>
                                <td>
                                    <div align="center">
                                        片/支</div>
                                </td>
                                <td>
                                    <div align="center">
                                        备注</div>
                                </td>
                                <td>
                                    <div align="center">
                                        检查内容</div>
                                </td>
                                <td>
                                    <div align="center">
                                        质检数量</div>
                                </td>
                                <td>
                                    <div align="center">
                                        自检报告</div>
                                </td>
                                <td>
                                    <div align="center">
                                        检验结果</div>
                                </td>
                                <td>
                                    <div align="center">
                                        不合格项</div>
                                </td>
                                <td>
                                    <div align="center">
                                        整改内容</div>
                                </td>
                                <td>
                                    <div align="center">
                                        质检人</div>
                                </td>
                                <td>
                                    <div align="center">
                                        检查日期</div>
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <div align="center">
                                    <asp:Label ID="Labeliszj" runat="server" Style="display: none; float: right" />
                                    <asp:CheckBox ID="CheckBox1" runat="server" /><%#Container.ItemIndex+1%></div>
                                <asp:Label ID="LabelKey" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                <asp:Label ID="LabelAgain" runat="server" Text='<%#Eval("ISAGAIN")%>' Visible="false"></asp:Label>
                                <asp:Label ID="LabelIsResult" runat="server" Text='<%#Eval("ISRESULT")%>' Visible="false"></asp:Label>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>'></asp:Label></div>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:Label ID="LabelMarName" runat="server" Text='<%#Eval("marnm")%>' Width="70px"></asp:Label></div>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:TextBox ID="TextBoxDrawingNO" runat="server" Width="100px" Text='<%#Eval("tuhao")%>'></asp:TextBox>
                                    <%-- <asp:Label ID="LabelDrawingNO" runat="server" Text='<%#Eval("tuhao")%>' Width="100px"></asp:Label>--%></div>
                            </td>
                            <td>
                                <asp:TextBox ID="TextMarid" runat="server" Text='<%#Eval("marid")%>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBOXgg" runat="server" Text='<%#Eval("margg")%>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBOXcz" runat="server" Text='<%#Eval("marcz")%>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBOXgb" runat="server" Text='<%#Eval("margb")%>'></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:Label ID="LabelNum" runat="server" Text='<%#Eval("cgnum")%>'></asp:Label></div>
                            </td>
                            <%--  <td  visible="false">
                                <div align="center">
                                    <asp:Label ID="LabelSW" runat="server" Text='<%#Eval("singlewgh")%>'></asp:Label></div>
                            </td>
                            <td  visible="false">
                                <div align="center">
                                    <asp:Label ID="LabelSumW" runat="server" Text='<%#Eval("sumwgh")%>'></asp:Label></div>
                            </td>--%>
                            <td>
                                <div align="center">
                                    <asp:TextBox ID="TextBoxPJNum" runat="server" Text='<%#Eval("bjnum")%>'></asp:TextBox></div>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:TextBox ID="TextBoxPJPZ" runat="server" Text='<%#Eval("pjpz")%>'></asp:TextBox></div>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:TextBox ID="TextBoxPJNote" runat="server" Text='<%#Eval("note")%>'></asp:TextBox></div>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:Label ID="LabelBJSJ" runat="server" Text='<%#Eval("BJSJ")%>'></asp:Label></div>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:TextBox ID="TextBoxState" runat="server" Text='<%#Eval("JHSTATE")%>' name="TextBoxState"
                                        onchange="Getjhstate(this,12)"></asp:TextBox></div>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:TextBox ID="TextBoxControlContent" runat="server" Text='<%#Eval("CONT")%>' onchange="Getjhstate(this,13)"></asp:TextBox></div>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:TextBox ID="TextBoxQCNum" runat="server" Width="60px" Text='<%#Eval("QCNUM")%>'></asp:TextBox></div>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:TextBox ID="TextBoxReport" runat="server" Text='<%#Eval("ZJREPORT")%>'></asp:TextBox></div>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:DropDownList ID="DropDownListResult" runat="server" SelectedValue='<%#DataBinder.Eval(Container.DataItem,"RESULT")==""?"待检":DataBinder.Eval(Container.DataItem,"RESULT") %>'
                                        Width="80px">
                                        <asp:ListItem Value="待检">待检</asp:ListItem>
                                        <asp:ListItem Value="报废">报废</asp:ListItem>
                                        <asp:ListItem Value="整改">整改</asp:ListItem>
                                        <asp:ListItem Value="待定">待定</asp:ListItem>
                                        <asp:ListItem Value="让步接收">让步接收</asp:ListItem>
                                        <asp:ListItem Value="部分合格">部分合格</asp:ListItem>
                                        <asp:ListItem Value="合格">合格</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:DropDownList ID="DropDownListBHGITEM" runat="server" SelectedValue='<%#DataBinder.Eval(Container.DataItem,"BHGITEM")==""?"无":DataBinder.Eval(Container.DataItem,"BHGITEM") %>'
                                        Width="80px">
                                        <asp:ListItem Value="无">无</asp:ListItem>
                                        <asp:ListItem Value="材质">材质</asp:ListItem>
                                        <asp:ListItem Value="硬度">硬度</asp:ListItem>
                                        <asp:ListItem Value="探伤">探伤</asp:ListItem>
                                        <asp:ListItem Value="尺寸">尺寸</asp:ListItem>
                                        <asp:ListItem Value="外观">外观</asp:ListItem>
                                        <asp:ListItem Value="铸件外观">铸件外观</asp:ListItem>
                                        <asp:ListItem Value="锻件外观">锻件外观</asp:ListItem>
                                        <asp:ListItem Value="资料不全">资料不全</asp:ListItem>
                                        <asp:ListItem Value="不符合标准">不符合标准</asp:ListItem>
                                        <asp:ListItem Value="缺货">缺货</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:TextBox ID="TextBoxContent" runat="server" Text='<%#Eval("ZGCONTENT")%>'></asp:TextBox></div>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:TextBox ID="TextBoxQCMan" runat="server" name="TextBoxQCMan" Text='<%#Eval("QCMANNM")%>'></asp:TextBox></div>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:TextBox ID="TextBoxDate" runat="server" Text='<%#Eval("QCDATE")%>' name="TextBoxDate"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxDate"
                                        TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd" FirstDayOfWeek="Monday">
                                    </asp:CalendarExtender>
                                </div>
                            </td>
                            <td id="tdshape" runat="server" visible="false">
                                <asp:TextBox ID="TextBOXshape" runat="server" Width="60px"></asp:TextBox>
                            </td>
                            <td id="tdisdc" runat="server" visible="false">
                                <asp:TextBox ID="TextBOXdc" runat="server" Width="60px"></asp:TextBox>
                            </td>
                            <td id="tdlength" runat="server" visible="false">
                                <asp:TextBox ID="TextBOlength" runat="server" Width="60px"></asp:TextBox>
                            </td>
                            <td id="tdwidth" runat="server" visible="false">
                                <asp:TextBox ID="TextBOXwidth" runat="server" Width="60px"></asp:TextBox>
                            </td>
                            <td id="tdunit" runat="server" visible="false">
                                <asp:TextBox ID="TextBOXunit" runat="server" Width="60px"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr class="baseGadget">
                            <td>
                            </td>
                            <td>
                                合计:
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="labelshuliang" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labeldanzhong" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labelzongzhong" runat="server"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
            <asp:Panel ID="Panel_Inspec" runat="server">
                <table>
                    <tr>
                        <td width="100">
                        </td>
                        <td width="900">
                            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                                width="100%">
                                <tr>
                                    <td align="center" colspan="2">
                                        <strong style="font-size: x-large; color: #FF0000;">最新质检记录</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="center">
                                        质检负责人:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="LabelQCMan" runat="server" Text=""></asp:Label>
                                    </td>
                                    <asp:HiddenField ID="HiddenFieldQCUniqueID" runat="server" />
                                </tr>
                                <tr>
                                    <td width="30%" align="center">
                                        记录人:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="LabelRecoder" runat="server" Text=""></asp:Label>
                                    </td>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                </tr>
                                <tr>
                                    <td width="30%" align="center">
                                        最终结果:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownListEndResult" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListEndResult_SelecedIndexChanged">
                                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="0">不合格</asp:ListItem>
                                            <asp:ListItem Value="1">合格</asp:ListItem>
                                            <asp:ListItem Value="2">关闭</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DropDownListEndResult"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="center">
                                        附件:
                                    </td>
                                    <td align="left">
                                        <div>
                                            <asp:FileUpload ID="FileUploadupdate" runat="server" />
                                            <asp:Button ID="bntupload" runat="server" Text="上 传" OnClick="bntupload_Click" CausesValidation="False" />
                                            <br />
                                            <asp:Label ID="filesError" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                                            <asp:Label ID="lbreport" runat="server" Visible="False"></asp:Label>
                                            <asp:GridView ID="gvfileslist" runat="server" AutoGenerateColumns="False" PageSize="5"
                                                CssClass="toptable grid" CellPadding="4" DataKeyNames="RP_ID" ForeColor="#333333"
                                                Width="60%">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="RP_FILENAME" HeaderText="文件名称">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RP_UPLOADDATE" HeaderText="上传时间">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="删除">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtndelete" runat="server" ImageUrl="~/Assets/images/erase.gif"
                                                                OnClick="imgbtndelete_Click" CausesValidation="False" ToolTip="删除" Width="15px"
                                                                Height="15px" />
                                                        </ItemTemplate>
                                                        <ControlStyle Font-Size="Small" />
                                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="下载">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtndownload" runat="server" ImageUrl="~/Assets/images/pdf.jpg"
                                                                OnClick="imgbtndownload_Click" CausesValidation="False" ToolTip="下载" Width="15px"
                                                                Height="15px" />
                                                        </ItemTemplate>
                                                        <ControlStyle Font-Size="Small" />
                                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Font-Size="Small"
                                                    Height="10px" />
                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="center">
                                        检验说明:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBoxDecp" runat="server" TextMode="MultiLine" Height="50px" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="center">
                                        备注:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBoxResultNote" runat="server" TextMode="MultiLine" Height="50px"
                                            Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="100">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Panel_Process" runat="server">
                <table>
                    <tr>
                        <td width="100">
                        </td>
                        <td width="900">
                            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                                width="100%">
                                <tr>
                                    <td width="100%" align="center">
                                        <strong style="font-size: x-large; color: #FF0000;">过程检验</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%" align="center">
                                        <asp:GridView ID="GridView1" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ISR_INSPCTOR" HeaderText="质检人" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="质检结果">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_result" runat="server" Text='<%# Eval("ISR_RESULT").ToString()=="1"?"合格":"不合格" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ISR_DATE" HeaderText="质检日期" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="交工报告">
                                                    <ItemTemplate>
                                                        <asp:Panel ID="Panel_Add_Report" runat="server">
                                                            <asp:Label ID="lb_jgreport" runat="server" Text='<%# Bind("ISR_JGREPORT") %>' Visible="false"></asp:Label>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="自检记录">
                                                    <ItemTemplate>
                                                        <asp:Panel ID="Panel_Add_ZJReport" runat="server">
                                                            <asp:Label ID="lb_zjreport" runat="server" Text='<%# Bind("ISR_ZJREPORT") %>' Visible="false"></asp:Label>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ISR_INSPCTDSCP" HeaderText="检验说明" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ISR_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Font-Size="Small"
                                                Height="10px" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="100">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
    <%--    <script type="text/javascript">
var width=$("#table1").width();
var height=$("#table1").height();

if(width<=1300)
{
$("div.fakeContainer").width($("#table1").width()+50);
}
else
{
$("div.fakeContainer").width(1300);
}
if(height<=350)
{
$("div.fakeContainer").height($("#table1").height());
}
else
{
$("div.fakeContainer").height(350);
}
(function() {
	var mySt = new superTable("table1", {
		cssSkin : "sSky",
		fixedCols :1,
	headerRows : 2,
		onStart : function () {
			
		},
		onFinish : function () {
		   for (var i=0, j=this.sDataTable.tBodies[0].rows.length; i<j; i++) 
           {
                 this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
                 {
                    var clicked = false;
                    var dataRow = this.sDataTable.tBodies[0].rows[i];
                    var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                    return function () 
                      {
                            if (clicked) 
                            {
                                    dataRow.style.backgroundColor = "#fff";
                                    fixedRow.style.backgroundColor = "#e4ecf7";
                                    clicked = false;
                                
                            }
                            else 
                            {
                                dataRow.style.backgroundColor = "#eeeeee";
                                fixedRow.style.backgroundColor = "#adadad";
                                clicked = true;
                            }
                        }          
                 }.call(this, i);
           }
		   
		}
	});
})();
//]]>
//     $("div.sFData>div.sFDataInner>table input").empty();
      $("div.sFData>div.sFDataInner>table input:text").remove();
      $("div.sFData>div.sFDataInner>table select").remove();
      $("div.sData>table input:checkbox").remove();

    </script>--%>
</asp:Content>
