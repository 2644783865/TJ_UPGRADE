<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Mar_Statistics.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Mar_Statistics"
    Title="采购物料汇总" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function redirectw(obj) {
            var sheetno;
            sheetno = obj.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            window.open("PC_TBPC_PurOrder.aspx?orderno=" + sheetno + "");
        }

    </script>
        
    <script language="javascript" type="text/javascript">
        function sTable() {
            var myST = new superTable("tabl", {
                cssSkin: "tDefault",
                headerRows: 1,
                fixedCols: 8,
                onStart: function() {
                },
                onFinish: function() {
                    
                    }
            });
        }

        $(function() {
            sTable();
        });
    </script>
    <script type="text/javascript">
//        function Statistic(obj) {
//            var totalhsje = 0;
//            var tab = document.getElementById("tabl");
//            for (i = 1; i < (tab.rows.length - 1); i++)
//            {
//                if(tab.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked)
//                {
//                    tab.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].parentNode.parentNode.parentNode.style.backgroundColor ='LawnGreen'; 
//                    var valhsje = tab.rows[i].getElementsByTagName("td")[12].getElementsByTagName("span")[0].innerHTML;
//                    totalhsje += parseFloat(valhsje);
//                }
//                else
//                {
//                tab.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].parentNode.parentNode.parentNode.style.backgroundColor =""; 
//                }
//            }
//              var $hsjehj=$("#table1 tr:first td:last span:eq(0)");
//              $hsjehj.html(totalhsje.toFixed(4));
//        }
        var totalhsje = 0;
        var totalnum=0;
        var uprice=0;
        function Statistic(obj) {
            var tab = document.getElementById("tabl");
            var valhsje = obj.parentNode.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("span")[0].innerHTML;
            var num=obj.parentNode.parentNode.parentNode.getElementsByTagName("td")[14].getElementsByTagName("span")[0].innerHTML;
            if(obj.checked)
            {
                obj.parentNode.parentNode.parentNode.style.backgroundColor ='LawnGreen'; 
                totalhsje += parseFloat(valhsje);
                totalnum+=parseFloat(num);
            }
            else
            {
                obj.parentNode.parentNode.parentNode.style.backgroundColor ="";
                totalhsje=totalhsje-parseFloat(valhsje);
                totalnum=totalnum-parseFloat(num);
            }
              var $hsjehj=$("#table1 tr:first td:last span:eq(0)");
              var $numhj=$("#table1 tr:first td:last span:eq(1)");
              var $averageuprice=$("#table1 tr:first td:last span:eq(2)");
              if(totalnum>0)
              {
                  uprice=totalhsje/totalnum;
              }
              $hsjehj.html(totalhsje.toFixed(4));
              $numhj.html(totalnum.toFixed(4));
              $averageuprice.html(uprice.toFixed(4));
        }
        
//      function seleAll() {
//            $(".cpbox.xscroll :checkbox[checked='true']").click();
//            if ($("#selectall").attr("checked")) {
//                var s = $(".cpbox.xscroll :checkbox").length;
//                $(".cpbox.xscroll :checkbox:lt(" + s + ")").click();
//            }
//            Statistic();
//        }

//        function seleLian() {
//            var a = $(".cpbox.xscroll :checkbox[checked='true']");
//            if (a.length == "2") {
//                nmin = $(".cpbox.xscroll :checkbox").index(a.eq(0));
//                nmax = $(".cpbox.xscroll :checkbox").index(a.eq(1)) - nmin - 1;
//                $(".cpbox.xscroll :checkbox:gt(" + nmin + "):lt(" + nmax + ")").click();
//            }
//            Statistic();
//        }

//        function seleCancel() {
//            $(".cpbox.xscroll :checkbox[checked='true']").click();
//            Statistic();
//        }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
              <div>
                 <div>
                   <div>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                 <asp:TextBox ID="tb_orderno" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                 供应商：<asp:TextBox ID="tb_supply" runat="server" Width="120px" AutoPostBack="true" OnTextChanged="tb_supply_Textchanged"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tb_supply"
                                        ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="5" MinimumPrefixLength="1"
                                        CompletionInterval="10" ServiceMethod="GetCusupinfo" FirstRowSelected="true" >
                                    </cc1:AutoCompleteExtender>
                                 &nbsp;&nbsp;&nbsp;
                                 开始日期：<asp:TextBox ID="tb_StartTime" Width="90px" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月dd日"
                                        ID="DateTextBox_CalendarExtender" runat="server" Format="yyyy-MM-dd" TargetControlID="tb_StartTime">
                                    </asp:CalendarExtender>
                                 &nbsp;
                                 结束日期：<asp:TextBox ID="tb_EndTime" Width="90px" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月dd日"
                                        ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="tb_EndTime">
                                    </asp:CalendarExtender>
                                 &nbsp;&nbsp;&nbsp;
                                 物料名称：<asp:TextBox ID="tb_name" Width="85px" runat="server"></asp:TextBox>
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:TextBox ID="tb_marid" Width="100px" runat="server" Visible="false"></asp:TextBox>
                                 &nbsp;&nbsp;&nbsp;
                                 计划跟踪号：<asp:TextBox ID="tb_ptc" Width="150px" runat="server"></asp:TextBox>
                                 &nbsp;&nbsp;
                                 出库任务号：<asp:TextBox ID="tb_outrwh" Width="80px" runat="server"></asp:TextBox>
                              </td>
                          </tr>
                          <tr>
                              <td>
                                 规格模糊查询：<asp:TextBox ID="tb_gg" Width="80px" runat="server"></asp:TextBox>
                                 材质：<asp:TextBox ID="tb_caizhi" Width="80px" runat="server"></asp:TextBox>
                                 物料类型：<asp:TextBox ID="tb_type" Width="60px" runat="server"></asp:TextBox>
                                 质检结果：<asp:DropDownList ID="DropDownList3" Width="100px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownList3_click">
                                        <asp:ListItem Text="-请选择-" Value="%" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="未报检" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="待检" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="报废" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="整改" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="待定" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="让步接收" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="部分合格" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="合格" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="免检" Value="8"></asp:ListItem>
                                    </asp:DropDownList>
                            
                                 &nbsp;&nbsp;&nbsp;
                                 出库结果：<asp:DropDownList ID="DropDownListckjg" Width="100px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownListckjg_click">
                                        <asp:ListItem Text="-请选择-" Value="%" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="未按项目" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="按项目" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                    采购员：<asp:DropDownList ID="DropDownListcgy" Width="100px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownListcgy_click">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                 <asp:RadioButtonList runat="server" RepeatLayout="Flow" ID="rbl_state" AutoPostBack="true" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="btn_search_click">
                                    <asp:ListItem Text="全部" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="未到货" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="已到货" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                                
                                &nbsp;&nbsp;&nbsp;   
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                物料多规格查询(以/间隔)：<asp:TextBox ID="txt_multiguige" Width="80px" runat="server"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; 
                                <asp:Button runat="server" ID="btnNameQuery" ForeColor="red" BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" Text="查询" OnClick="btnNameQuery_OnClick" />
                            </td>
                        </tr>
                    </table>
                 </div>
              </div>
           </div>
            <div>
                <div>
                    <div style="height: 475px; overflow: auto; width: 100%">
                        <div class="cpbox xscroll">
                            <table id="tabl" class="nowrap cptable fullwidth" align="center">
                                <asp:Repeater ID="Purordertotal_list_Repeater" runat="server" OnItemDataBound="Purordertotal_list_Repeater_ItemDataBound">
                                    <HeaderTemplate>
                                        <tr align="center" style="background-color: #B9D3EE">
                                            <td>
                                                
                                            </td>
                                            <td>
                                                <strong>行号</strong>
                                            </td>
                                            <%--<td>
                                                <strong>单据编号</strong>
                                            </td>--%>
                                            <td>
                                                <strong>采购员</strong>
                                            </td>
                                            <td>
                                                <strong>订单日期</strong>
                                            </td>
                                            <td>
                                                <strong>订单总金额</strong>
                                            </td>
                                            <td>
                                                <strong>供应商</strong>
                                            </td>
                                            <%--<td>
                                                <strong>采购合同号</strong>
                                            </td>--%>
                                            <td>
                                                <strong>计划跟踪号</strong>
                                            </td>
                                            <td>
                                                <strong>出库任务号</strong>
                                            </td>
                                            <td>
                                                <strong>部件名称</strong>
                                            </td>
                                            <td>
                                                <strong>材料编码</strong>
                                            </td>
                                            <td>
                                                <strong>名称</strong>
                                            </td>
                                            <td>
                                                <strong>含税单价</strong>
                                            </td>
                                            <td>
                                                <strong>含税金额</strong>
                                            </td>
                                            <td>
                                                <strong>单位</strong>
                                            </td>
                                            <td>
                                                <strong>数量</strong>
                                            </td>
                                            <td>
                                                <strong>辅助单位</strong>
                                            </td>
                                            <td>
                                                <strong>辅助数量</strong>
                                            </td>
                                            <td>
                                                <strong>规格</strong>
                                            </td>
                                            <td>
                                                <strong>材质</strong>
                                            </td>
                                            <td>
                                                <strong>长度</strong>
                                            </td>
                                            <td>
                                                <strong>宽度</strong>
                                            </td>
                                            <td>
                                                <strong>类型</strong>
                                            </td>
                                            <td>
                                                <strong>审核标志</strong>
                                            </td>
                                            <td>
                                                <strong>是否到货</strong>
                                            </td>
                                            <td>
                                                <strong>是否报检</strong>
                                            </td>
                                            <td>
                                                <strong>是否入库</strong>
                                            </td>
                                            <td>
                                                <strong>交货日期</strong>
                                            </td>
                                            <td>
                                                <strong>实际到货日期</strong>
                                            </td>
                                            <td>
                                                <strong>到货数量</strong>
                                            </td>
                                            <td>
                                                <strong>技术备注</strong>
                                            </td>
                                            <td>
                                                <strong>出库数量</strong>
                                            </td>
                                            <td>
                                                <strong>出库单价</strong>
                                            </td>
                                            <td>
                                                <strong>出库金额</strong>
                                            </td>
                                            <td>
                                                <strong>出库含税单价</strong>
                                            </td>
                                            <td>
                                                <strong>出库含税金额</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr id="row" runat="server" ondblclick="redirectw(this)">
                                            <td>
                                                &nbsp;&nbsp;
                                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" onclick="Statistic(this)" BorderStyle="None" runat="server">
                                                </asp:CheckBox>
                                            </td>
                                            <td id="td2" runat="server" style="width:5px">
                                                <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                                <asp:HiddenField ID="PO_CODE" runat="server" Value='<%#Eval("orderno")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="PO_ZDID" runat="server" Text='<%#Eval("zdrid")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="PO_ZDNM" runat="server" Text='<%#Eval("zdrnm")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PO_SHTIME" runat="server" Text='<%#Eval("zdtime")==""?Eval("zdtime"):Convert.ToDateTime(Eval("zdtime")).ToString("yyyy-MM-dd")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="zje" runat="server" Text='<%#Eval("PO_ZJE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PO_SUPPLIERID" runat="server" Text='<%#Eval("supplierid")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="PO_SUPPLIERNM" runat="server" Text='<%#Eval("suppliernm")%>'></asp:Label>
                                            </td>
                                            <%--<td id="tdHT">
                                                <asp:HyperLink ID="Hyphth" runat="server" Target="_blank" title="点击查看采购合同信息">
                                                    <asp:Label ID="hetonghao" runat="server"></asp:Label>
                                                </asp:HyperLink>
                                            </td>--%>
                                            
                                            <td>
                                                <asp:Label ID="ptcode" runat="server" Text='<%#Eval("ptcode")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_ptcode" runat="server" Width="150px" Text='<%#Eval("ptcode")%>'
                                                    ForeColor="#1A438E" BorderStyle="None" Style="background-color: Transparent;
                                                    text-align: center" ToolTip='<%#Eval("ptcode")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbtsaid" runat="server" Text='<%#Eval("OP_TSAID")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="PO_CHILDENGNAME" Text='<%#Eval("PO_CHILDENGNAME") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_CHILDENGNAME" runat="server" Text='<%#Eval("PO_CHILDENGNAME")%>'
                                                    BorderStyle="None" Style="background-color: Transparent; text-align: center"
                                                    ForeColor="#1A438E" ToolTip='<%#Eval("PO_CHILDENGNAME")%>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="marid" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="marnm" runat="server" Text='<%#Eval("marnm")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_marnm" runat="server" Text='<%#Eval("marnm")%>' BorderStyle="None"
                                                    ForeColor="#1A438E" Style="background-color: Transparent; text-align: center"
                                                    ToolTip='<%#Eval("marnm")%>' Width="80px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="ctprice" runat="server" Text='<%#Eval("ctprice")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="ctamount" runat="server" Text='<%#Eval("ctamount")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="marunit" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="zxnum" runat="server" Text='<%#Eval("zxnum")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PO_TECUNIT" runat="server" Text='<%#Eval("PO_TECUNIT")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="fznum" runat="server" Text='<%#Eval("fznum")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="margg" runat="server" Text='<%#Eval("margg")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_margg" runat="server" Text='<%#Eval("margg")%>' BorderStyle="None"
                                                    ForeColor="#1A438E" Style="background-color: Transparent; text-align: center"
                                                    ToolTip='<%#Eval("margg")%>' Width="80px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="marcz" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="length" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="width" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PO_MASHAPE" runat="server" Text='<%#Eval("PO_MASHAPE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PO_shbz" runat="server" Text='<%#get_ordlistsh_state(Eval("totalstate").ToString())%>'></asp:Label>
                                                <asp:Label ID="PO_STATE" runat="server" Text='<%#Eval("totalstate")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="PO_cstate" runat="server" Text='<%#Eval("detailcstate")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="detailstate" runat="server" Text='<%#Eval("detailstate")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="daohuoF" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="Hypzlbj" runat="server" Target="_blank" title="点击查看报检信息">
                                                    <asp:Label ID="zlbj" runat="server" Text='<%#get_zlbj(Eval("PO_CGFS").ToString())%>'></asp:Label>
                                                </asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:Label ID="rukuF" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="cgtimerq" runat="server" Text='<%#Eval("cgtimerq")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="recdate" runat="server" Text='<%#Eval("recdate")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="recgdnum" runat="server" Text='<%#Eval("recgdnum")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="detailnote" runat="server" Width="150px" Text='<%#Eval("detailnote")%>'
                                                    ForeColor="#1A438E" BorderStyle="None" Style="background-color: Transparent;
                                                    text-align: center" ToolTip='<%#Eval("detailnote")%>'></asp:TextBox>
                                            </td>
                                            
                                            
                                            
                                            <td>
                                                <asp:Label ID="OP_REALNUM" runat="server" Text='<%#Eval("OP_REALNUM")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="OP_UPRICE" runat="server" Text='<%#Eval("OP_UPRICE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="OP_HSUPRICE" runat="server" Text='<%#Eval("OP_HSUPRICE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="OP_AMOUNT" runat="server" Text='<%#Eval("OP_AMOUNT")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="OP_HSAMOUNT" runat="server" Text='<%#Eval("OP_HSAMOUNT")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td colspan="42" align="center">
                                        <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                            没有记录！</asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <uc1:UCPaging ID="UCPaging1" runat="server" />
                        </div>
                    </div>
                    <table width="100%" id="table1">
                        <tr>
                            <td align="left">
                                <%--<input type="checkbox" onclick="seleAll()" id="selectall" /><label for="selectall">&nbsp;全选</label>&nbsp;&nbsp;
                                <input type="button" value="连选" onclick="seleLian()" />
                                <input type="button" value="取消" onclick="seleCancel()" />--%>
                            </td>
                            <td>
                                筛选结果：共<asp:Label ID="lb_select_num" runat="server" Text=""></asp:Label>条记录&nbsp;&nbsp;&nbsp;
                                合计金额：<asp:Label ID="lb_select_money" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;
                                合计数量：<asp:Label ID="lb_select_sl" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                &nbsp;&nbsp;&nbsp;
                                含税单价：<asp:Label ID="lbdduprice" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                勾选合计：<asp:Label ID="lb_checkhj" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;数量：<asp:Label ID="lb_num" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;含税单价：<asp:Label ID="lb_uprice" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                            </td>
                            <td>
                                出库含税金额合计：<asp:Label ID="lb_ckhsjehj" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;
                                出库金额合计：<asp:Label ID="lb_ckjehj" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;
                                出库数量合计：<asp:Label ID="lb_ckslhj" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;出库含税单价：<asp:Label ID="lbckprice" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
</asp:Content>
