<%@ Page Language="C#" MasterPageFile="~/masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="QROut_List.aspx.cs" Inherits="ZCZJ_DPF.QR_Interface.QROut_List" Title="扫码出库物料管理" %>
<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>

    <script type="text/javascript" language="javascript">
function confirmapp() {
        window.returnValue = true;
        window.close(); 
}

function closewin() {

if(window.opener!=null)
{
   window.opener.location = window.opener.location.href;
}
    window.close();
}

function storageexport(type,time) {

 var date=new Date();
var time=date.getTime();

    var retVal = window.showModalDialog("SM_WarehouseStorage_Export.aspx?type="+type, "", "dialogWidth=650px;dialogHeight=400px;help=no;scroll=no");
}

      
function viewCondition(){
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
}




         //初始化弹窗
        $(function() {
            $("#win").show().dialog({
                title: '分配项目',
                width: 700,
                height: 500,
                closed: true,
                cache: false,
                modal: true,
                buttons: '#buttons'
            });
        });
        //初始化数据
        function showAssigedPlanno(obj) {
            var QROut_ID=obj.parentNode.parentNode.getElementsByTagName("td")[0].getElementsByTagName("input")[2].value;
            var MaterialCode=obj.parentNode.parentNode.getElementsByTagName("td")[0].getElementsByTagName("input")[3].value;
            var outnum=obj.parentNode.parentNode.getElementsByTagName("td")[0].getElementsByTagName("input")[4].value;
            $('#hidQRID').val(QROut_ID);
            $('#hidoutnum').val(outnum);
            $("#win").dialog("open");
            $('#dg').datagrid({
                        type: "GET",
                        url:'PTCAssignedAjaxHandler.aspx?method=getOutPTC&MaterialCode='+MaterialCode,
                        pagination:false,
                        loadMsg: 'Loading……',
		                autoRowHeight:true,
		                remoteSort:true,
		                nowrap:true,
		                rownumbers: true,
		                singleSelect:true,
                        columns:[[
                            {field:'sqcode',hidden:'true'},
                            {field:'ptc',title:'计划跟踪号',align:'center',width:200},
		                    {field:'materialcode',title:'物料编码',align:'center',width:100},
		                    {field:'materialname',title:'物料名称',align:'center',width:100},
		                    {field:'materialstandard',title:'型号规格',align:'center',width:50},
		                    {field:'attribute',title:'材质',align:'center',width:50},
		                    {field:'stonum',title:'库存数量',align:'center',width:60}
	                    ]]
                  });
        }
        function savePlanno() {
            var QROut_ID=$('#hidQRID').val();
            var outnum=parseFloat($('#hidoutnum').val());
            var row = $('#dg').datagrid('getSelected');
            var SQCODE="";
			if (row){
			    SQCODE=row.sqcode;
			    stonum=parseFloat(row.stonum);
			    if(stonum<outnum)
			    {
			        alert("库存数少于出库数！");
			        return;
			    }
			    $.ajax({
                    type: "POST",
                    url: 'PTCAssignedAjaxHandler.aspx',
                    data: {"QROut_ID":QROut_ID,"SQCODE":SQCODE,"method":"AssignedOutPTC" },
                    success: function(msg) {
                        if (msg == "true") {
                            alert("添加成功！");
                        } else {
                            alert("添加失败！");
                        }
                        $("#win").dialog("close");
                        window.location.reload();
                    }
                });
			}
			else
			{
			    alert("请选择相应数据！");
			}
        }
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <asp:HiddenField ID="hfdtn" runat="server" />
                <asp:HiddenField ID="hfdtp" runat="server" />
                <table width="98%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
                            &nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="hplfjcb" runat="server" NavigateUrl="~/SM_Data/SM_MatCompare.aspx"
                        Target="_blank" Font-Underline="false">
                        <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                    runat="server" Visible="false" /><%--物料比对--%></asp:HyperLink>&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="right">
                            <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;
                            <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                Y="10">
                            </asp:ModalPopupExtender>
                            <input id="StorageExport" type="button" value="导出" onclick="storageexport()" runat="server"
                                visible="false" />
                            <asp:Button ID="BtnShowExport" runat="server" Text="导出" OnClick="BtnShowExport_Click" Visible="false" />&nbsp;
                            <asp:Button ID="btn_llout" runat="server" Text="生产领料库单" OnCommand="btn_out_Command"
                                CommandArgument="0" />
                            <input id="Close" type="button" value="关闭" onclick="closewin()" runat="server" Visible="false" />&nbsp;
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition" runat="server" Width="100%" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                        <Triggers><asp:PostBackTrigger ControlID="QueryButton" /></Triggers>
                        <Triggers><asp:PostBackTrigger ControlID="btnReset" /></Triggers>
                        <ContentTemplate>
                            <table width="100%" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td style="white-space: nowrap;" align="left" colspan="2">
                                        &nbsp;&nbsp;&nbsp;显示条数：<asp:TextBox ID="TextBoxCount" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap;" align="left">
                                        &nbsp;
                                    </td>
                                    <td style="white-space: nowrap;" align="left">
                                        <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="查询" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap; width: 25%;" align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;仓库：<asp:DropDownList ID="WarehouseDropDownList"
                                            runat="server" OnSelectedIndexChanged="WarehouseDropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="white-space: nowrap; width: 25%;" align="left">
                                        &nbsp;&nbsp;&nbsp;子仓库：<asp:DropDownList ID="ChildWarehouseDropDownList" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="white-space: nowrap; width: 25%;" align="left">
                                        &nbsp;&nbsp;&nbsp;物料种类：<asp:DropDownList ID="TypeDropDownList" runat="server" OnSelectedIndexChanged="TypeDropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="white-space: nowrap; width: 25%;" align="left">
                                        物料子类：<asp:DropDownList ID="SubTypeDropDownList" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;截止日期：<asp:TextBox ID="DateTextBox" Enabled="false" runat="server"></asp:TextBox><asp:CalendarExtender
                                                        ID="DateTextBox_CalendarExtender" runat="server" Format="yyyy-MM-dd" TargetControlID="DateTextBox" Enabled="false">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;物料代码：<asp:TextBox ID="TextBoxCode" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;物料名称：<asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;规格型号：<asp:TextBox ID="TextBoxStandard" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;材&nbsp;&nbsp;&nbsp;质：<asp:TextBox ID="TextBoxAttribute"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;标识号：<asp:TextBox ID="TextBoxNO" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    计划跟踪号：<asp:TextBox ID="TextBoxPTC" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;长：<asp:TextBox
                                                        ID="TextBoxLength" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;宽：<asp:TextBox
                                                        ID="TextBoxWidth" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;批&nbsp;&nbsp;&nbsp;号：<asp:TextBox ID="TextBoxLotNumber"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                     &nbsp;&nbsp;&nbsp;是否定尺：<asp:TextBox ID="TextBoxFixed" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                              <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：<asp:TextBox ID="TextBoxNote"
                                                        runat="server"></asp:TextBox>
                                              </td>
                                               <td>
                                                 &nbsp;&nbsp;&nbsp;订单编号：<asp:TextBox ID="TextBoxOrderCode" runat="server"></asp:TextBox>                                                       
                                              </td>                                         
                                              
                                            </tr>
                                            <tr>
                                              <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;出库状态：   
                                                <asp:DropDownList ID="DropDownListQROutState" runat="server">
                                                   <asp:ListItem Value="0" Selected="True">未出库</asp:ListItem>
                                                   <asp:ListItem Value="1">已出库</asp:ListItem>
                                                   <asp:ListItem Value="">全部</asp:ListItem>
                                                </asp:DropDownList>
                                              </td>
                                              <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;逻&nbsp;&nbsp;&nbsp;辑：<asp:DropDownList ID="DropDownListFatherLogic"
                                                        runat="server">
                                                        <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                        <asp:ListItem Value="OR">或者</asp:ListItem>
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                              </td>                                            
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="right">
                                                    项目分配状态：
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="DropDownListPlanno" runat="server">
                                                        <asp:ListItem Value="0">未分配</asp:ListItem>
                                                        <asp:ListItem Value="1">已分配</asp:ListItem>
                                                        <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
                    <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                        <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                            没有相关物料信息!</asp:Panel>
                        <table id="GridView1" width="1000px" align="center" cellpadding="4" cellspacing="1" class="toptable grid nowrap"
                        border="1">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <HeaderTemplate>
                                    <tr>
                                        <td>
                                            序号
                                        </td>
                                        <td>
                                            分配项目
                                        </td>
                                        <td>
                                            计划跟踪号
                                        </td>
                                        <td>
                                            物料代码
                                        </td>
                                        <td>
                                            物料名称
                                        </td>
                                        <td>
                                            规格型号
                                        </td>
                                        <td>
                                            材质
                                        </td>
                                        <td>
                                            是否定尺
                                        </td>
                                        <td>
                                            长
                                        </td>
                                        <td>
                                            宽
                                        </td>
                                        <td>
                                            国标
                                        </td>
                                        <td>
                                            单位
                                        </td>
                                        <td>
                                            库存数量
                                        </td>
                                        <td>
                                            张(支)
                                        </td>
                                        <td>
                                            出库任务号
                                        </td>
                                        <td>
                                            出库数量
                                        </td>
                                        <td>
                                            扫码时间
                                        </td>
                                        <td>
                                            出库状态
                                        </td>
                                        <td>
                                            备注
                                        </td>
                                        <td>
                                            仓库名称
                                        </td>
                                        <td>
                                            仓位名称
                                        </td>
                                        <td>
                                            订单编号
                                        </td>
                                        <td>
                                            计划模式
                                        </td>
                                        <td>
                                            标识号
                                        </td>
                                        
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" runat="server" /><%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                            <asp:Label ID="LabelSQCODE" runat="server" Text='<%#Eval("SQCODE")%>' Visible="false"></asp:Label>
                                            <asp:HiddenField ID="hid_ptc" runat="server" Value='<%#Eval("PTC")%>' />
                                            <asp:Label ID="lb_symbol" runat="server" Width="25px" Height="12px" Visible="false" ToolTip="该项目提过物料代用"></asp:Label>
                                            <input id="QROut_ID" runat="server" type="hidden" value='<%#Eval("QROut_ID")%>' />
                                            <input id="QROut_MatCode" runat="server" type="hidden" value='<%#Eval("QROut_MatCode")%>' />
                                            <input id="QROut_Num" runat="server" type="hidden" value='<%#Eval("QROut_Num")%>' />
                                            <asp:Label ID="QROut_ID2" runat="server" Text='<%#Eval("QROut_ID")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HypAssigedPlanno" runat="server" onClick="showAssigedPlanno(this)">
                                                <asp:Image ID="Image5" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                    align="absmiddle" runat="server" />
                                                添加
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                            <%#Eval("PTC")%>
                                        </td>
                                        <td>
                                            <%#Eval("QROut_MatCode")%>
                                        </td>
                                        <td>
                                            <%#Eval("MaterialName")%>
                                        </td>
                                        <td>
                                            <%#Eval("MaterialStandard")%>
                                        </td>
                                        <td>
                                            <%#Eval("Attribute")%>
                                        </td>
                                        <td>
                                            <%#Eval("Fixed")%>
                                        </td>
                                        <td>
                                            <%#Eval("Length")%>
                                        </td>
                                        <td>
                                            <%#Eval("Width")%>
                                        </td>
                                        <td>
                                            <%#Eval("GB")%>
                                        </td>
                                        <td>
                                            <%#Eval("Unit")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelNumber" runat="server" Text='<%#Eval("Number")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelQuantity" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelTaskID" runat="server" Text='<%#Eval("QROut_TaskID")%>'></asp:Label>
                                            <asp:Label ID="LabelQROut_ID" Visible="false" runat="server" Text='<%#Eval("QROut_ID")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelQROut_Num" runat="server" Text='<%#Eval("QROut_Num")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelQROut_Time" runat="server" Text='<%#Eval("QROut_Time")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelQROut_State" runat="server" Text='<%#Eval("QROut_State")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="QROut_Note" runat="server" ForeColor="#1A438E" Style="background-color: Transparent;text-align: center" Width="150px" Text='<%#Eval("QROut_Note")%>' ToolTip='<%#Eval("QROut_Note")%>'></asp:TextBox>
                                        </td>
                                        
                                        <td>
                                            <%#Eval("Warehouse")%>
                                        </td>
                                        <td>
                                            <%#Eval("Position")%>
                                        </td>
                                        <td>
                                            <%#Eval("OrderCode")%>
                                        </td>
                                        <td>
                                            <%#Eval("PlanMode")%>
                                        </td>
                                        <td>
                                            <%#Eval("CGMODE")%>
                                        </td>
                                        
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            总计：
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
                                        <td>
                                            <asp:Label ID="LabelTN" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelTP" runat="server" Text=""></asp:Label>
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
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                        </table>
                       </div>
                    </asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
    <div>
        <div id="win" visible="false">
            <strong>选择项目：</strong>
            <input type="hidden" id="hidQRID" value="" />
            <input type="hidden" id="hidoutnum" value="" />
            <div>
                <table id="dg">
                </table>
            </div>
        </div>
        <div id="buttons" style="text-align: right; display: none">
            <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePlanno();">
                保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                    onclick="javascript:$('#win').dialog('close')">取消</a>
        </div>
    </div>
</asp:Content>
