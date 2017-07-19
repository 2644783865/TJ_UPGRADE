<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="PC_TBPC_Purchaseplan_detail.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchaseplan_detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    采购任务   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
 <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
     <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
 <script src="../JS/KeyControl.js" type="text/javascript" charset="GB2312" language="javascript"></script>
    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>
     <script src="../JS/DatePicker.js" type="text/javascript"></script>
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>
   
    <style type="text/css"> 
     .autocomplete_completionListElement 
     {  
     	margin : 0px; 
     	background-color : #1C86EE; 
     	color : windowtext; 
     	cursor : 'default'; 
     	text-align : left; 
     	list-style:none; 
     	padding:0px;
        border: solid 1px gray; 
        width:400px!important;   
     }
     .autocomplete_listItem 
     {   
     	border-style : solid; 
     	border :#FFEFDB; 
     	border-width : 1px;  
     	background-color : #EEDC82; 
     	color : windowtext;  
     } 
     .autocomplete_highlightedListItem 
     { 
     	background-color: #1C86EE; 
     	color: black; 
     	padding: 1px; 
     } 
  </style> 
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
	        fixedCols : 3,
//	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
	        onStart : function () 
	        {
//		        this.start = new Date();
	        },
	        onFinish : function () 
	        {
//		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
	        }
        });
        }
         function openclosemodewin()
        {
       var shape="<%=gloabshape%>";
       var orderno="<%=gloabsheetno%>";
       var autonum=Math.round(10000*Math.random());
//       window.showModalDialog('PC_Date_closemarshow.aspx?autonum='+autonum+'&orderno='+escape(orderno)+'&shape='+escape(shape)+'','',"dialogHeight:500px;dialogWidth:1200px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
       window.open("PC_Date_closemarshow.aspx?autonum="+autonum+"&shape="+escape(shape)+"&orderno="+escape(orderno)+"");
        }
       
   </script>  
    
    <asp:Label ID="ControlFinder" runat="server"  Visible="false"></asp:Label>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

                   <div>                        
                                <table id="tab1" width="100%">
                                <tr>
                                <td>计划跟踪号 ：<asp:TextBox ID="planid" runat="server"></asp:TextBox></td>
                                <td>名&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;称：<asp:TextBox ID="mc" runat="server"></asp:TextBox></td>
                                <td>国&nbsp;&nbsp;&nbsp;标：<asp:TextBox ID="gb" runat="server"></asp:TextBox></td>
                                <td>材料编码：<asp:TextBox ID="clbm" runat="server"></asp:TextBox></td>
                                <td>规&nbsp;&nbsp;&nbsp;格：<asp:TextBox ID="gg" runat="server"></asp:TextBox></td>
                                <td>材&nbsp;&nbsp;&nbsp;质：<asp:TextBox ID="cz" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                <td>图号/标识号：<asp:TextBox ID="tuhao" runat="server"></asp:TextBox></td>
                                <td>材料类型：<asp:DropDownList ID="cllx" runat="server"></asp:DropDownList></td>
                                <td>分工人：<asp:DropDownList ID="fgr" runat="server"></asp:DropDownList></td>
                                <td>分工日期：<input id="fgrq" runat="server" type="text" onclick="setday(this)" value="" readonly="readonly" /></td>
                                <td>采购员：<asp:DropDownList ID="cgy" runat="server"></asp:DropDownList></td>
                                <td>申请人：<asp:DropDownList ID="sqr" runat="server"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                <td>执行人：<asp:DropDownList ID="zxr" runat="server"></asp:DropDownList></td>
                                <td>执行日期：<input id="zxrq" runat="server" type="text" onclick="setday(this)" value="" readonly="readonly" /></td>
                                <td>是否生成比价单：<asp:DropDownList ID="sfbjd" runat="server">
                                <asp:ListItem Text="请选择" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                <asp:ListItem Text="否" Value="2"></asp:ListItem>
                                </asp:DropDownList></td>
                                 <td>是否生成订单：<asp:DropDownList ID="sfdd" runat="server">
                                <asp:ListItem Text="请选择" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                <asp:ListItem Text="否" Value="2"></asp:ListItem>
                                </asp:DropDownList></td>
                                <td><asp:Button ID="search" runat="server" Text="查询" onclick="search_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="clear" runat="server" Text="重置" onclick="clear_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="closesearch" runat="server" text="关闭查看" OnClientClick="openclosemodewin()" /></td>
                                </tr>
                                </table>
                   </div>
                            <div style="height: 470px; overflow: auto; width: 100%">
                            <div  class="cpbox xscroll">
                                     <table id="tab" width="100%" align="center" class="nowrap cptable fullwidth" border="1">
                                    <asp:Repeater ID="tbpc_purshaseplanassignRepeater" runat="server" OnItemDataBound="tbpc_purshaseplanassignRepeaterbind">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle"  style="background-color:#B9D3EE;">
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <strong>行号</strong>
                                                </td>
                                              <%--  <td id="ph" runat="server">
                                                    <strong>批号</strong>
                                                </td>
                                                <td id="xm" runat="server">
                                                    <strong>项目</strong>
                                                </td>
                                                <td id="gc" runat="server">
                                                    <strong>工程</strong>
                                                </td>--%>
                                                <td id="jhh" runat="server">
                                                    <strong>计划号</strong>
                                                </td>
                                                <td id="th" runat="server">
                                                    <strong>图号/标识号</strong>
                                                </td>
                                                <td id="clbm" runat="server">
                                                    <strong>材料编码</strong>
                                                </td>
                                                <td >
                                                    <strong>名称</strong>
                                                </td>
                                                <td >
                                                    <strong>规格</strong>
                                                </td>
                                                <td id="cz" runat="server">
                                                    <strong>材质</strong>
                                                </td>
                                                <td id="gb" runat="server">
                                                    <strong>国标</strong>
                                                </td>
                                               <td>
                                                    <strong>类型</strong>
                                                </td>
                                                <td >
                                                    <strong>采购数量</strong>
                                                </td>
                                                 <td>
                                                    <strong>单位</strong>
                                                </td>
                                                <td >
                                                    <strong>采购辅助数量</strong>
                                                </td>
                                                 <td >
                                                    <strong>辅助单位</strong>
                                                </td>
                                                 <td id="fgr" runat="server">
                                                    <strong>分工人</strong>
                                                </td>
                                                <td id="fgrq" runat="server">
                                                    <strong>分工日期</strong>
                                                </td>
                                                <td id="cgy" runat="server">
                                                    <strong>采购员</strong>
                                                </td>
                                                 <td id="sqr" runat="server">
                                                    <strong>申请人</strong>
                                                </td>
                                                <td >
                                                    <strong>执行数量</strong>
                                                </td>
                                                <td >
                                                    <strong>执行辅助数量</strong>
                                                </td>
                                                <td id="cd" runat="server">
                                                    <strong>长度</strong>
                                                </td>
                                                <td id="kd" runat="server">
                                                    <strong>宽度</strong>
                                                </td>
                                                <td id="zxr" runat="server">
                                                    <strong>执行人</strong>
                                                </td>
                                                <td id="zxrq" runat="server">
                                                    <strong>执行时间</strong>
                                                </td>
                                                <td id="Td1"  runat="server" visible="false">
                                                    <strong>关键部件</strong>
                                                </td>
                                                <td >
                                                    <strong>是否生成比价单</strong>
                                                </td>
                                                <td >
                                                    <strong>是否生成订单</strong>
                                                </td>
                                                <td id="td0" runat="server" visible="false" >
                                                    <strong>比价单号</strong>
                                                </td>
                                                <td id="td2" runat="server" visible="false" >
                                                    <strong>订单号</strong>
                                                </td>
                                                <td>
                                                    <strong>备注</strong>
                                                </td>
                                                <%--<td runat="server" visible="false">
                                                    <strong>查看变更</strong>
                                                </td>--%>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr id="row" runat="server" class="baseGadget" ondblclick="redirectw(this)" onclick="MouseClick1(this)">
                                                <td  runat="server" id="ch">
                                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        Checked="false" onclick="checkme(this)"></asp:CheckBox>&nbsp;
                                                      <asp:Label ID="PUR_ID" runat="server" Text='<%#Eval("PUR_ID")%>' Visible="false"></asp:Label>  
                                                </td>
                                                <td runat="server" id="ch1">
                                                   
                                                    <asp:Label ID="ROWSNUM" runat="server" Text='<%# Container.ItemIndex + 1+(Convert.ToDouble(UCPaging1.CurrentPage)-1)*100%>'></asp:Label>
                                                </td>
                                              <%--  <td id="ph1" runat="server">
                                                    <asp:Label ID="PUR_PCODE" runat="server" Text='<%#Eval("PUR_PCODE")%>'></asp:Label>
                                                </td>
                                                <td id="xm1" runat="server">
                                                   
                                                    <asp:Label ID="PUR_PJID" runat="server" Text='<%#Eval("PUR_PJID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="PUR_PJNAME" runat="server" Text='<%#Eval("PUR_PJNAME")%>'></asp:Label>
                                                  
                                                </td>
                                                <td id="gc1" runat="server">
                                                    <asp:Label ID="PUR_ENGID" runat="server" Text='<%#Eval("PUR_ENGID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="PUR_ENGNAME" runat="server" Text='<%#Eval("PUR_ENGNAME")%>'></asp:Label>
                                                </td>--%>
                                                <td id="jhh1" runat="server">
                                                    <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("PUR_PTCODE")%>'></asp:Label>
                                                </td>
                                                 <td id="th1" runat="server">
                                                    <asp:Label ID="PUR_TUHAO" runat="server" Text='<%#Eval("PUR_TUHAO")%>'></asp:Label>
                                                </td>
                                                <td id="clbm1" runat="server">
                                                    <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("PUR_MARID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("PUR_MARNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("PUR_MARNORM")%>'></asp:Label>
                                         
                                                </td>
                                                <td id="cz1" runat="server">
                                                    <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("PUR_MARTERIAL")%>'></asp:Label>
                                                </td>
                                                <td id="gb1" runat="server">
                                                    <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("PUR_GUOBIAO")%>'></asp:Label>
                                                </td>
                                                
                                                <td>
                                                    <asp:Label ID="PUR_MASHAPE" runat="server" Text='<%#Eval("PUR_MASHAPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_RPNUM" runat="server" Text='<%#Eval("PUR_RPNUM")%>'></asp:Label>
                                                </td>
                                                  <td>
                                                    
                                                    <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("PUR_NUNIT")%>'></asp:Label>
                                                   
                                                </td>
                                                <td>
                                                  
                                                    <asp:Label ID="PUR_RPFZNUM" runat="server" Text='<%#Eval("PUR_RPFZNUM")%>'></asp:Label>
                                                   
                                                </td>
                                                <td>
                                                
                                                    <asp:Label ID="FZUNIT" runat="server" Text='<%#Eval("FZUNIT")%>'></asp:Label>
                                                </td>
                                                <td id="fgr1" runat="server">
                                                    <asp:Label ID="PUR_PTASMAN" runat="server" Text='<%#Eval("PUR_PTASMAN")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td id="fgrq1" runat="server">
                                                    <asp:Label ID="PUR_PTASTIME" runat="server" Text='<%#Eval("PUR_PTASTIME")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td id="cgr1" runat="server">
                                                    <asp:Label ID="PUR_CGMAN" runat="server" Text='<%#Eval("PUR_CGMAN")%>' Visible="false"></asp:Label>
                                                    &nbsp;
                                                    <asp:Label ID="PUR_CGMANNM" runat="server" Text='<%#Eval("PUR_CGMANNM")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td id="sqr1" runat="server">
                                                    <asp:Label ID="PUR_SQRNM" runat="server" Text='<%#Eval("sqrnm")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="ZXNUM" runat="server" Text='<%#Eval("ZXNUM")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                 
                                                    <asp:Label ID="ZXFZNUM" runat="server" Text='<%#Eval("ZXFZNUM")%>'></asp:Label>&nbsp;
                                                </td>
                                                
                                                <td id="cd1" runat="server">
                                                  
                                                    <asp:Label ID="PUR_LENGTH" runat="server" Text='<%#Eval("PUR_LENGTH")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td id="kd1" runat="server">
                                              
                                                    <asp:Label ID="PUR_WIDTH" runat="server" Text='<%#Eval("PUR_WIDTH")%>'></asp:Label>
                                                </td>
                                              
                                                <td id="zxr1" runat="server">
                                                    <asp:Label ID="ICL_REVIEWA" runat="server" Text='<%#Eval("ICL_REVIEWA")%>' Visible="false"></asp:Label>
                                                    &nbsp;
                                                    <asp:Label ID="ICL_REVIEWANM" runat="server" Text='<%#Eval("ICL_REVIEWANM")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td id="zxrq1" runat="server">
                                                    <asp:Label ID="ICL_IQRDATE" runat="server" Text='<%#Eval("ICL_IQRDATE")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td id="Td3" runat="server" visible="false">
                                                    <asp:Label ID="PUR_KEYCOMS" runat="server" Text='<%#Eval("PUR_KEYCOMS")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="Hypbjd" runat="server" Target="_blank">
                                                        <asp:Label ID="PUR_BJD" runat="server" Text='<%#get_pur_bjd(Eval("PUR_STATE").ToString())%>'></asp:Label></asp:HyperLink>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="Hypdd" runat="server" Target="_blank">
                                                        <asp:Label ID="PUR_DD" runat="server" Text='<%#get_pur_dd(Eval("PUR_STATE").ToString())%>'></asp:Label></asp:HyperLink>
                                                    <asp:Label ID="PUR_STATE" runat="server" Text='<%#Eval("PUR_STATE")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td id="td0" runat="server" visible="false">
                                                    <asp:Label ID="PIC_SHEETNO" runat="server" Text='<%#Eval("PIC_SHEETNO")%>'></asp:Label>
                                                </td>
                                                <td id="td1" runat="server" visible="false">
                                                    <asp:Label ID="PO_SHEETNO" runat="server" Text='<%#Eval("PO_SHEETNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("PUR_NOTE")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                               <%-- <td runat="server" id="bedit">
                                                    <asp:HyperLink ID="Hypbiangeng" Target="_blank"  runat="server" ForeColor="Red" NavigateUrl='<%#"~/PC_Data/PC_TBPC_Purchange_detail.aspx?ptcode="+Eval("PUR_PTCODE")%>'>请点击</asp:HyperLink>
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                          <tr></tr>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    </table>

                                    <tr>
                                        <td colspan="31" align="center">
                                            <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                没有数据！</asp:Panel>
                                        </td>
                                    </tr>
                                  </table>
    <script language="javascript" type="text/javascript">   
        var myST = new superTable("tab", {
	        cssSkin : "tDefault",
	        headerRows : 1,
	        fixedCols : 3,
//	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
	        onStart : function () 
	        {
//		        this.start = new Date();
	        },
	        onFinish : function () 
	        {
//		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
            }
        });
   </script>  
                           

                            </div>
                            <div>
                             <uc1:UCPaging ID="UCPaging1" runat="server" />
                            </div>
                        </div>                       
    </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
