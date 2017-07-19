<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Otherpur_Bill_List.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Otherpur_Bill_List"
    Title="新增物料管理" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="../Assets/Setting.css" rel="stylesheet" type="text/css" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script language="javascript" type="text/javascript">
    function viewCondition()
    {
       document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
    }
    </script>

    <script language="javascript" type="text/javascript">  
   Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args)
        {
            
        }
        function EndRequestHandler(sender, args)
        {
         var myST = new superTable("tab", {
	        cssSkin : "tDefault",
	        headerRows : 1,
	        fixedCols : 0,
//	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
	        onStart : function () 
	        {
//		        this.start = new Date();
	        },
	        onFinish : function () 
	        {
	            for (var i=1, j=this.sDataTable.tBodies[0].rows.length-2; i<j; i++) 
                {
                    var dataRow = this.sDataTable.tBodies[0].rows[i+1];
                    var coltextbef=this.sDataTable.tBodies[0].rows[i].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                    var coltext=this.sDataTable.tBodies[0].rows[i+1].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                    if(coltext==coltextbef)
                    {
                         
                               dataRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0].style.display ="none";  
                    }
                }
//		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
	        }
        });
        }
       
    </script>

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="RightContent">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    
                                    <td>
                                        <asp:RadioButtonList ID="rbl_xiatui" runat="server" RepeatColumns="2" TextAlign="Right"
                                            AutoPostBack="true" OnSelectedIndexChanged="btn_search_click">
                                            <asp:ListItem Text="未下推" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="已下推" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rbl_shenhe" runat="server" RepeatColumns="6" TextAlign="Right"
                                            AutoPostBack="true" OnSelectedIndexChanged="btn_search1_click">
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="按部门查看："></asp:Label>
                                        <asp:DropDownList ID="dplBM" runat="server" AutoPostBack="true" OnSelectedIndexChanged="QueryButton_Click">
                                        </asp:DropDownList>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />
                                        &nbsp;
                                        <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                            PopupControlID="PanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                            Y="80">
                                        </asp:ModalPopupExtender>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="delete" runat="server" Text="删除" OnClick="delete_Click" />
                                        &nbsp;<asp:Button ID="btnBeiku" runat="server" Text="新增年度备库" OnClientClick="return confirm('确定新增本年度备库，以便新增采购信息？')"
                                            OnClick="btnBeiku_Click" />
                                        &nbsp;<asp:Button ID="btndaochu" runat="server" Text="导出" OnClick="btndaochu_Click" />
                                        <asp:HyperLink ID="addpcpurbill" CssClass="hand" runat="server">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Assets/icons/pcadd.gif" />
                                            新增采购申请</asp:HyperLink>&nbsp;&nbsp;
                                        <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="addpcpurbill"
                                            PopupControlID="palPSHTLB" Position="Bottom" OffsetY="4" OffsetX="-65" CacheDynamicResults="false">
                                        </asp:PopupControlExtender>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="palPSHTLB" Style="visibility: hidden; border-style: solid; border-width: 1px;
                                border-color: blue; background-color: Menu;" runat="server">
                                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>--%>
                                        <table width="170px">
                                            <tr>
                                                <td>
                                                    <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                        font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                        <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                            cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                            title="关闭">X</a>
                                                    </div>
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    采购申请类别:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dplPSHTLB_Select" runat="server">
                                                        <asp:ListItem Text="-请选择-" Value="%"></asp:ListItem>
                                                        <asp:ListItem Text="采购申请" Value="0"></asp:ListItem>
                                                        <%--<asp:ListItem Text="仓库备库" Value="1"></asp:ListItem>--%>
                                                        <%--<asp:ListItem Text="预提计划" Value="2"></asp:ListItem>
                                  <asp:ListItem Text="技术外协" Value="3"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    物料类型:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                                        <asp:ListItem Text="-请选择-" Value="%"></asp:ListItem>
                                                        <asp:ListItem Text="定尺板" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="非定尺板" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="型材" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="釆" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="锻" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="铸" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="采购成品" Value="6"></asp:ListItem>
                                                        <asp:ListItem Text="非" Value="7"></asp:ListItem>
                                                        <%--<asp:ListItem Text="电气电料" Value="8"></asp:ListItem>--%>
                                                        <asp:ListItem Text="油漆" Value="9"></asp:ListItem>
                                                        <asp:ListItem Text="其他" Value="10"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <br />
                                                    <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="btnConfirm_OnClick"
                                                        UseSubmitBehavior="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </asp:Panel>
                            <asp:Panel ID="PanelCondition" runat="server" Width="95%" Style="display: none">
                                <%--<asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                        <table width="95%" style="background-color: #CCCCFF; border: solid 1px black;">
                                            <tr>
                                                <td colspan="8" align="center">
                                                    <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="查询" />
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
                                                    &nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    单据编号：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tb_orderno" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    物料类型：
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownList2" runat="server">
                                                        <asp:ListItem Text="-请选择-" Value="%"></asp:ListItem>
                                                        <asp:ListItem Text="定尺板" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="非定尺板" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="型材" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="釆" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="锻" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="铸" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="采购成品" Value="6"></asp:ListItem>
                                                        <asp:ListItem Text="非" Value="7"></asp:ListItem>
                                                        <%--<asp:ListItem Text="电气电料" Value="8"></asp:ListItem>--%>
                                                        <asp:ListItem Text="油漆" Value="9"></asp:ListItem>
                                                        <asp:ListItem Text="其他" Value="10"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td align="right">
                                                    开始日期：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tb_StartTime" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="DateTextBox_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                                        TargetControlID="tb_StartTime">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    结束日期：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tb_EndTime" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="tb_EndTime">
                                                    </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    名称：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    材质：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tb_cz" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    规格：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tb_gg" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    国标：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tb_gb" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    项目：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tb_eng" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    制单人：
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="drp_stu" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right">
                                                    备注：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtdetailnote" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div style="height: 450px; overflow: auto; width: 100%">
                            <div class="cpbox2 xscroll">
                                <table id="tab" align="center" class="nowrap cptable fullwidth">
                                    <asp:Repeater ID="tbpc_otherpurbill_list_Repeater" runat="server" OnItemDataBound="tbpc_otherpurbill_list_Repeater_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                <td>
                                                    <strong>行号</strong>
                                                </td>
                                                <td>
                                                    <strong>单据编号</strong>
                                                </td>
                                                <td>
                                                    <strong>物料编码</strong>
                                                </td>
                                                <td>
                                                    <strong>名称</strong>
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
                                                    <strong>数量</strong>
                                                </td>
                                                <td>
                                                    <strong>单位</strong>
                                                </td>
                                                <td>
                                                    <strong>日期</strong>
                                                </td>
                                                <td>
                                                    <strong>使用部门</strong>
                                                </td>
                                                <td>
                                                    <strong>制单人</strong>
                                                </td>
                                                <td>
                                                    <strong>下推状态</strong>
                                                </td>
                                                <td>
                                                    <strong>审批状态</strong>
                                                </td>
                                                <td>
                                                    <strong>备注</strong>
                                                </td>
                                                <td runat="server" id="hedit">
                                                    <strong>修改</strong>
                                                </td>
                                                <td runat="server" id="hlookup">
                                                    <strong>查看</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr id="row" runat="server" class="baseGadget" onmouseover="this.className='highlight'"
                                                onmouseout="this.className='baseGadget'">
                                                <td>
                                                    <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        onclick="checkme(this)" Checked="false"></asp:CheckBox>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PCODE" runat="server" Text='<%#Eval("PCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MARID" runat="server" Text='<%#Eval("MARID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MARNM" runat="server" Text='<%#Eval("MARNM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MARGG" runat="server" Text='<%#Eval("MARGG")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MARCZ" runat="server" Text='<%#Eval("MARCZ")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MARGB" runat="server" Text='<%#Eval("MARGB")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MARNUM" runat="server" Text='<%#Eval("NUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MARUNIT" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="SUBMITTM" runat="server" Text='<%#Eval("SUBMITTM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="USEDEPNAME" runat="server" Text='<%#Eval("USEDEPNAME")%>'></asp:Label>
                                                    <asp:Label ID="USEDEPID" runat="server" Text='<%#Eval("USEDEPID")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="SUBMITID" runat="server" Text='<%#Eval("SUBMITID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="SUBMITNM" runat="server" Text='<%#Eval("SUBMITNM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MP_STATE" runat="server" Text='<%#Eval("STATE")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="MP_STATETEXT" runat="server" Text='<%#get_pr_state(Eval("STATE").ToString())%>'></asp:Label>
                                                    <asp:HiddenField ID="DETAILSTATE" runat="server" Value='<%#Eval("DETAILSTATE")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="MP_SPZT" runat="server" Text='<%#Eval("SPZT")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="MP_SPZTTEXT" runat="server" Text='<%#get_spzt(Eval("SPZT").ToString())%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="DETAILNOTE" runat="server" Text='<%#Eval("DETAILNOTE")%>'></asp:Label>
                                                </td>
                                                <td runat="server" id="bedit">
                                                    <asp:HyperLink ID="hyp_edit" runat="server">
                                                        <asp:Label ID="Label1" runat="server" Text="修改"></asp:Label></asp:HyperLink>
                                                    <%--<asp:HyperLink ID="hyp_edit" runat="server" ForeColor="Red" NavigateUrl='<%#"~/PC_Data/PC_TBPC_Otherpur_Bill_edit.aspx?action=edit&mp_id="+Eval("PCODE")+"%>'>修改</asp:HyperLink>--%>
                                                </td>
                                                <td runat="server" id="blookup">
                                                    <%--<asp:HyperLink ID="HyperLink_lookup" runat="server" ForeColor="Red" NavigateUrl='<%#"~/PC_Data/PC_TBPC_Otherpur_Bill_look.aspx?action=view&mp_id="+Eval("PCODE")+"%>'>查看</asp:HyperLink>--%>
                                                    <asp:HyperLink ID="HyperLink_lookup" runat="server" Target="_blank">
                                                        <asp:Label ID="PUR_DD" runat="server" Text="查看"></asp:Label></asp:HyperLink>
                                                        <asp:Label ID="MP_IFFAST" runat="server" Text='<%#Eval("MP_IFFAST")%>' Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="16" align="center">
                                            <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                没有记录！</asp:Panel>
                                        </td>
                                    </tr>
                                </table>

                                <script language="javascript" type="text/javascript">   
        var myST = new superTable("tab", {
	        cssSkin : "tDefault",
	        headerRows : 1,
	        fixedCols : 0,
//	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
	        onStart : function () 
	        {
//		        this.start = new Date();
	        },
	        onFinish : function () 
	        {	             
                for (var i=1, j=this.sDataTable.tBodies[0].rows.length-2; i<j; i++) 
                {
                    var dataRow = this.sDataTable.tBodies[0].rows[i+1];
                    var coltextbef=this.sDataTable.tBodies[0].rows[i].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                    var coltext=this.sDataTable.tBodies[0].rows[i+1].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                    if(coltext==coltextbef)
                    {
                         
                               dataRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0].style.display ="none";  
                        
                    }
                }
                 
//		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
            }
        });
                                </script>

                            </div>
                            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                没有记录!</asp:Panel>
                            <uc1:UCPaging ID="UCPaging1" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
