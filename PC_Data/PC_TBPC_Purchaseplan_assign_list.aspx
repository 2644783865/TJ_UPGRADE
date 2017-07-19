<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchaseplan_assign_list.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchaseplan_assign_list"
    Title="任务分工" %>

<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    任务分工
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script type="text/javascript">
        function document.onreadystatechange() {
            if (document.readyState == "complete") {
                var ptc = "<%=gloabptc%>";
                var col = 5;
                if (ptc != "") {
                    othptcfind(ptc, col)
                }

            }
        }
    </script>

    <script type="text/javascript">
        function redirectw(obj) {
            var sheetno;
            sheetno = obj.getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
            window.location.href = "PC_TBPC_Purchaseplan_assign_detail.aspx?sheetno=" + escape(sheetno);
            //window.location.href="PC_TBPC_Purchaseplan_startcontent.aspx?mp_id="+sheetno;
        }
    </script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {

        }
        function EndRequestHandler(sender, args) {
            var myST = new superTable("tab", {
                cssSkin: "sDefault",
                headerRows: 1,
                fixedCols: 3,
                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                onStart: function() {
                    //		        this.start = new Date();
                },
                onFinish: function() {
                    //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                }
            });
        }
       
    </script>

    <script type="text/javascript">
        var PTC;
        var allPTC = "";
        //初始化选人窗口
        $(function() {
            $('#win').show().dialog({
                title: '请填写驳回理由',
                width: 350,
                height: 250,
                closed: true,
                cache: false,
                modal: true,
                buttons: '#buttons'
            });
        });

        $(function() {
            $("#btnBackTask").click(function() {
                var $check = $("input[type='checkbox']:checked");
                if ($check.length < 1) {
                    alert("请选择一条数据驳回！");
                    return;
                }
                else {

                    $check.each(function() {
                        var $tr = $(this).parent().parent().parent();
                        //console.log($tr);
                        //console.log($tr.find("input[type='hidden']")[2]);
                        var state = $($tr.find("input[type='hidden']")[2]).val();
                        if (state == 3 || state == 4) {
                            //  console.log($tr.find("span[name*='PUR_PTCODE']"));
                           // PTC = $tr.find("span[name*='PUR_PTCODE']").html();
                            PTC = $($tr.find("input[type='hidden']")[3]).val();
                            //console.log(PTC); 
                            alert(PTC);
                            allPTC += PTC + "|";

                        }
                        else {
                            alert("已比价或已生成订单的数据无法驳回！");
                            return;
                        }
                    });
                    console.log(allPTC);
                    $("#win").dialog("open");
                }
            });

        });

        function savePick() {
            var txtNote = $("#txtNote").val();
            $("#aSave").linkbutton('disable');
            $.ajax({
                type: "POST",
                url: "PC_AjaxHandler.aspx",
                data: { ptc: allPTC, txtNote: txtNote, method: "BackTask" },
                dataType: "json",
                success: function(data) {
                    //                    alert(data);
                    //                    var objData = eval("(" + data + ")");
                    //                    console.log(objData);
                    $("#win").dialog("close");
                    alert(data.msg);
                    window.location = "PC_TBPC_Purchaseplan_assign_list.aspx";
                }
            });
        }
        
        function viewCondition() {
            document.getElementById("<%=PanelCondition.ClientID%>").style.display = 'block';
        }
    </script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                任务列表：
                                <asp:RadioButton ID="rad_stall" runat="server" Text="全部" TextAlign="Right" GroupName="selectgroup"
                                    OnCheckedChanged="rad_stall_CheckedChanged" AutoPostBack="true" />
                                <asp:RadioButton ID="rad_weifengong" runat="server" Text="未分工" TextAlign="Right"
                                    GroupName="selectgroup" OnCheckedChanged="rad_weifengong_CheckedChanged" AutoPostBack="True"
                                    Checked="true" />
                                <asp:RadioButton ID="rad_yifengong" runat="server" Text="已分工" TextAlign="Right" GroupName="selectgroup"
                                    OnCheckedChanged="rad_yifengong_CheckedChanged" AutoPostBack="True" />
                                <asp:RadioButton ID="rad_stwzx" runat="server" Text="未执行" TextAlign="Right" GroupName="selectgroup"
                                    OnCheckedChanged="rad_stwzx_CheckedChanged" AutoPostBack="true" />
                                <asp:RadioButton ID="rad_stxbj" runat="server" Text="询比价" TextAlign="Right" GroupName="selectgroup"
                                    Visible="false" OnCheckedChanged="rad_stxbj_CheckedChanged" AutoPostBack="true" />
                                <asp:RadioButton ID="rad_stxdd" runat="server" Text="下订单" TextAlign="Right" GroupName="selectgroup"
                                    Visible="false" OnCheckedChanged="rad_stxdd_CheckedChanged" AutoPostBack="true" />
                                <asp:RadioButton ID="rad_closelist" runat="server" Text="已关闭" TextAlign="Right" GroupName="selectgroup"
                                    OnCheckedChanged="rad_closelist_CheckedChanged" AutoPostBack="true" />
                                <asp:RadioButton ID="rad_yzanting" runat="server" Text="已暂停" TextAlign="Right" GroupName="selectgroup"
                                    OnCheckedChanged="rad_yzanting_CheckedChanged" AutoPostBack="true" />
                            </td>
                            <td>
                                排序方式：
                                <asp:RadioButton ID="rad_ptcode" runat="server" Text="计划跟踪号" TextAlign="Right" GroupName="selectgroup1"
                                    OnCheckedChanged="rad_ptcode_CheckedChanged" AutoPostBack="true" Checked="true" />
                                <asp:RadioButton ID="rad_wlcode" runat="server" Text="物料编码" TextAlign="Right" GroupName="selectgroup1"
                                    OnCheckedChanged="rad_wlcode_CheckedChanged" AutoPostBack="true" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:HyperLink ID="hpybohuilook" runat="server" Target="_blank" NavigateUrl="~/TM_Data/TM_MP_Back.aspx">
                                    <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" />已驳回物料查看</asp:HyperLink>
                            </td>
                            <td align="right">
                                <input type="button" id="btnBackTask" value="驳回任务" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_back" runat="server" Text="返回仓库" OnClientClick="viewCondition()"
                                    Visible="false" />
                                <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btn_back"
                                    PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                    Y="70" X="900">
                                </asp:ModalPopupExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_hclose" runat="server" Text="行关闭" OnClick="btn_hclose_Click"
                                    Visible="false" />&nbsp;&nbsp;
                                <asp:Button ID="btn_zanting" runat="server" Text="任务暂停" OnClick="btn_zanting_Click"
                                    Visible="false" />&nbsp;&nbsp;
                                <asp:Button ID="btn_qxzanting" runat="server" Text="取消暂停" OnClick="btn_qxzanting_Click"
                                    Visible="false" />&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelCondition" runat="server" Width="280px" Style="display: none">
                        <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="280px" style="background-color: #CCCCFF; border: solid 1px black;">
                                    <tr>
                                        <td colspan="8" align="center">
                                            <asp:Button ID="QueryButton" runat="server" OnClick="btn_back_Click" Text="确认" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="取消" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            理由：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbreason" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div>
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        物料名称:
                                        <asp:TextBox ID="Tb_PJID" runat="server" Text="" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="Tb_PJNAME" runat="server" Width="70px" Text=""></asp:TextBox>
                                        规格:
                                        <asp:TextBox ID="tb_guige" runat="server" Width="50px" Text=""></asp:TextBox>
                                        计划跟踪号:
                                        <asp:TextBox ID="Tb_ENGID" runat="server" Text="" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="Tb_ENGNAME" runat="server" Text=""></asp:TextBox>
                                        批号:
                                        <asp:TextBox ID="Tb_pcode" runat="server" Text=""></asp:TextBox>
                                        开始日期:<asp:TextBox ID="tb_riqif" runat="server" Text="" Width="90px"></asp:TextBox>
                                        结束日期:<asp:TextBox ID="tb_riqit" runat="server" Text="" Width="90px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tb_riqif"
                                            Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月dd日">
                                        </cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tb_riqit"
                                            Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月dd日">
                                        </cc1:CalendarExtender>
                                        采购员:<asp:DropDownList ID="drp_stu" runat="server">
                                        </asp:DropDownList>
                                        <asp:Button ID="btn_search" runat="server" Text="查询" OnClick="btn_search_click" />
                                        <asp:Button ID="btn_clear" runat="server" Text="清除" OnClick="btn_clear_click" />
                                        <%--<asp:Button ID="btn_filter" runat="server" Text="过滤" OnClientClick="openmodewin()"/>
                                        <asp:TextBox ID="hid_filter" runat="server" Style="display: none"></asp:TextBox>--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="height: 405px; overflow: auto; width: 100%">
                            <div class="cpbox3 xscroll">
                                <table id="tab" class="nowrap cptable fullwidth" align="center">
                                    <asp:Repeater ID="tbpc_pchsplanassign_listtRepeater" runat="server" OnItemDataBound="tbpc_pchsplanassign_listtRepeater_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <strong>行号</strong>
                                                </td>
                                                <td>
                                                    <strong>批号</strong>
                                                </td>
                                                <td>
                                                    <strong>设备</strong>
                                                </td>
                                                <td>
                                                    <strong>物料名称</strong>
                                                </td>
                                                <td>
                                                    <strong>材料类型</strong>
                                                </td>
                                                <td>
                                                    <strong>规格</strong>
                                                </td>
                                                <td>
                                                    <strong>材质</strong>
                                                </td>
                                                <td>
                                                    <strong>国标</strong>
                                                </td>
                                                <td>
                                                    <strong>图号</strong>
                                                </td>
                                                <td>
                                                    <strong>采购数量</strong>
                                                </td>
                                                <td>
                                                    <strong>单位</strong>
                                                </td>
                                                <td>
                                                    <strong>辅助数量</strong>
                                                </td>
                                                <td>
                                                    <strong>辅助单位</strong>
                                                </td>
                                                <td>
                                                    <strong>分工人</strong>
                                                </td>
                                                <td>
                                                    <strong>日期</strong>
                                                </td>
                                                <td>
                                                    <strong>采购员</strong>
                                                </td>
                                                <td>
                                                    <strong>申请人</strong>
                                                </td>
                                                <td>
                                                    <strong>是否分工</strong>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <strong>是否生成比价单</strong>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <strong>是否生成订单</strong>
                                                </td>
                                                <td id="td0" runat="server" visible="false">
                                                    <strong>比价单号</strong>
                                                </td>
                                                <td id="td1" runat="server" visible="false">
                                                    <strong>订单号</strong>
                                                </td>
                                                <td>
                                                    <strong>备注</strong>
                                                </td>
                                                <td>
                                                    <strong>项目</strong>
                                                </td>
                                                <td>
                                                    <strong>长度</strong>
                                                </td>
                                                <td>
                                                    <strong>宽度</strong>
                                                </td>
                                                <td>
                                                    <strong>计划跟踪号</strong>
                                                </td>
                                                <td>
                                                    <strong>材料编码</strong>
                                                </td>
                                                <td>
                                                    <strong>取消分工状态</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr id="row" runat="server" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)"
                                                ondblclick="redirectw(this)">
                                                <td>
                                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        Checked="false" onclick="checkme(this)"></asp:CheckBox>&nbsp;
                                                    <asp:HiddenField runat="server" ID="Hid_PurId" Value='<%#Eval("PUR_ID") %>' />
                                                    <asp:HiddenField runat="server" ID="Hid_MaShape" Value='<%#Eval("PUR_MASHAPE") %>' />
                                                    <input type="hidden" runat="server" id="PUR_STATE" value='<%#Eval("PUR_STATE")%>' />
                                                    
                                                    <asp:Label ID="lbjiaji" runat="server" Text="加急" Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="ROWSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_PCODE" runat="server" Text='<%#Eval("PUR_PCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_ENGID" runat="server" Text='<%#Eval("PUR_ENGID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="PUR_ENGNAME" runat="server" Text='<%#Eval("PUR_ENGNAME")%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txt_PUR_ENGNAME" runat="server" Text='<%#Eval("PUR_ENGNAME")%>'
                                                        ForeColor="#1A438E" BorderStyle="None" Style="background-color: Transparent;
                                                        text-align: center" ToolTip='<%#Eval("PUR_ENGNAME")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("PUR_MARNAME")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MASHAPE" runat="server" Text='<%#Eval("PUR_MASHAPE")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("PUR_MARNORM")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("PUR_MARTERIAL")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("PUR_GUOBIAO")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_TUHAO" runat="server" Text='<%#Eval("PUR_TUHAO")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_RPNUM" runat="server" Text='<%#Eval("PUR_RPNUM")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("PUR_NUNIT")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_RPFZNUM" runat="server" Text='<%#Eval("PUR_RPFZNUM")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="marfzunit" runat="server" Text='<%#Eval("marfzunit")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_PTASMAN" runat="server" Text='<%#Eval("PUR_PTASMAN")%>' Visible="false"></asp:Label>&nbsp;
                                                    <asp:Label ID="PUR_PTASMANNM" runat="server" Text='<%#Eval("PUR_PTASMANNM")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_PTASTIME" runat="server" Text='<%#Eval("PUR_PTASTIME")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_CGMAN" runat="server" Text='<%#Eval("PUR_CGMAN")%>' Visible="false"></asp:Label>&nbsp;
                                                    <asp:Label ID="PUR_CGMANNM" runat="server" Text='<%#Eval("PUR_CGMANNM")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <%#Eval("sqrnm")%>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="Hypfg" runat="server" Target="_blank">
                                                        <asp:Label ID="PUR_FG" runat="server" Text='<%#get_pur_fg(Eval("PUR_STATE").ToString())%>'></asp:Label></asp:HyperLink>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <asp:HyperLink ID="Hypbjd" runat="server" Target="_blank">
                                                        <asp:Label ID="PUR_BJD" runat="server" Text='<%#get_pur_bjd(Eval("PUR_STATE").ToString())%>'></asp:Label></asp:HyperLink>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <asp:HyperLink ID="Hypdd" runat="server" Target="_blank">
                                                        <asp:Label ID="PUR_DD" runat="server" Text='<%#get_pur_dd(Eval("PUR_STATE").ToString())%>'></asp:Label></asp:HyperLink>
                                                </td>
                                                <td id="td0" runat="server" visible="false">
                                                    <asp:Label ID="PIC_SHEETNO" runat="server" Text='<%#Eval("PIC_SHEETNO")%>'></asp:Label>
                                                </td>
                                                <td id="td1" runat="server" visible="false">
                                                    <asp:Label ID="PO_SHEETNO" runat="server" Text='<%#Eval("PO_SHEETNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("PUR_NOTE")%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtNote" runat="server" Text='<%#Eval("PUR_NOTE")%>' ForeColor="#1A438E"
                                                        BorderStyle="None" Style="background-color: Transparent; text-align: center"
                                                        ToolTip='<%#Eval("PUR_NOTE")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_PJID" runat="server" Text='<%#Eval("PUR_PJID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="PUR_PJNAME" runat="server" Text='<%#Eval("PUR_PJNAME")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("PUR_PTCODE")%>' name="PUR_PTCODE"></asp:Label><input
                                                        type="hidden" runat="server" id="hidptcode" value='<%#Eval("PUR_PTCODE")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("PUR_MARID")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_QUXIAOREASON" runat="server" Text='<%#Eval("PUR_QUXIAOREASON")%>'></asp:Label>&nbsp;
                                                    
                                                    <asp:Label ID="PUR_IFFAST" runat="server" Text='<%#Eval("PUR_IFFAST")%>' Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="26" align="center">
                                            <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                没有数据！</asp:Panel>
                                        </td>
                                    </tr>
                                </table>

                                <script language="javascript" type="text/javascript">
                                    var myST = new superTable("tab", {
                                        cssSkin: "sDefault",
                                        headerRows: 1,
                                        fixedCols: 3,
                                        //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                                        onStart: function() {
                                            //		        this.start = new Date();
                                        },
                                        onFinish: function() {
                                            //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                                        }
                                    });
                                </script>

                            </div>
                            <div>
                                <table width="100%" border="1" class="nowrap cptable fullwidth" align="center">
                                    <tr>
                                        <td width="20%" align="left">
                                            <asp:CheckBox ID="selectall" runat="server" Text="全选" AutoPostBack="true" TextAlign="left"
                                                Checked="false" OnCheckedChanged="selectall_CheckedChanged" />
                                            <asp:Button ID="btn_LX" runat="server" Text="连选" OnClick="btn_LX_click" />
                                            <asp:Button ID="btn_QX" runat="server" Text="取消" OnClick="btn_QX_click" />
                                        </td>
                                        <td>
                                            共<asp:Label ID="lb_count" runat="server" Text="Label"></asp:Label>条记录&nbsp; 共<asp:Label
                                                ID="lb_page" runat="server" Text="Label"></asp:Label>页 &nbsp; 当前第<asp:Label ID="lb_CurrentPage"
                                                    runat="server" Text="1"></asp:Label>页&nbsp;
                                            <asp:LinkButton ID="LinkFirst" runat="server" OnClick="LinkFirst_Click">第一页</asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="LinkUp" runat="server" OnClick="LinkUp_Click">上一页</asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkDown" runat="server" OnClick="LinkDown_Click">下一页</asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkLast" runat="server" OnClick="LinkLast_Click">最后一页</asp:LinkButton>&nbsp;
                                            转到第<asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                            页
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="win" visible="false">
        <textarea id="txtNote" style="height: 150px; width: 330px"></textarea>
    </div>
    <div id="buttons" style="text-align: right" visible="false">
        <a id="aSave" class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true"
            onclick="savePick();">保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#win').dialog('close')">取消</a>
        <input id="hidPerson" type="hidden" value="" />
    </div>
</asp:Content>
