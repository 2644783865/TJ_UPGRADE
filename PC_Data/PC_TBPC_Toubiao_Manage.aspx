<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_TBPC_Toubiao_Manage.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Toubiao_Manage"  MasterPageFile="~/Masters/RightCotentMaster.Master" Title="招标物料管理"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    招标采购任务  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
 <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    function redirectw(obj)
    {
        var sheetno;
        sheetno=obj.getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
        //window.location.href="PC_TBPC_Purchaseplan_assign_detail.aspx?sheetno="+sheetno;
        window.location.href="PC_TBPC_Purchaseplan_startcontent.aspx?mp_id="+escape(sheetno);
    }
    
    function openmodewin()
    {   
        var num1;
        var num2;
        radio11=document.getElementById("ctl00_PrimaryContent_rad_all");
        radio12=document.getElementById("ctl00_PrimaryContent_rad_mypart");
        
        radio21=document.getElementById("ctl00_PrimaryContent_rad_stall");
        radio22=document.getElementById("ctl00_PrimaryContent_rad_stwzx");
        radio23=document.getElementById("ctl00_PrimaryContent_rad_stxdd");
        
      
        if(radio11.checked==true)
        {
            num1=1;
        }
        else
        {
            num1=2;
        }
        
        if(radio21.checked==true)
        {
            num2=1;
        }
        else if(radio22.checked==true)
        {
            num2=2;
        }
        else if(radio23.checked==true)
        {
            num2=3;
        }
       
        var tablenmid_code;
        var autonum=Math.round(10000*Math.random());
        tablenmid_code=document.getElementById("<%=hid_filter.ClientID %>").value;
        window.showModalDialog('PC_TBPC_Itemsfilter.aspx?autonum='+autonum+'&tablenmid_code='+tablenmid_code+'','',"dialogHeight:460px;dialogWidth:680px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
        window.location.href="PC_TBPC_Toubiao_Manage.aspx?num1="+num1+"&num2="+num2+"";
    }
    
    function mowinopen(ptcode_rcode)
    {
       var autonum=Math.round(10000*Math.random());
       window.open('PC_Data_addto_list.aspx?zb=1&autonum='+autonum+'&ptcode_rcode='+escape(ptcode_rcode)+'','',"dialogHeight:460px;dialogWidth:780px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
       window.location.href="PC_TBPC_Toubiao_Manage.aspx";
    }
    function ddopen(mpcode)
    {
        var autonum=Math.round(10000*Math.random());
        window.open("TBPC_Purorderdetail_xiugai.aspx?orderno=" + mpcode + "");
    }
    
//    self.moveTo(0,0)
//    self.resizeTo(screen.availWidth,screen.availHeight)
    </script>
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
	        cssSkin : "sDefault",
	        headerRows : 1,
	        fixedCols : 0,
//	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
	        onStart : function () 
	        {
		        var ptc="<%=gloabptc%>";
                if(ptc!="")
                {
                    var col=5;
                    var table=document.getElementById("tab"); 
                    var tr=table.getElementsByTagName("tr"); 
                    for( i=1;i <tr.length-1;i++) 
                    { 
                       if(tr[i].getElementsByTagName("td")[col].getElementsByTagName("span")[0].innerHTML==ptc)
                        { 
                            tr[i].style.backgroundColor ='Orange'; 
                           
                        }
                        else 
                        {
                            tr[i].style.backgroundColor ='';
                        }
                     }  
                 }      
	        },
	        onFinish : function () 
	        {
//		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
	        }
        });
        }
       
   </script>  
    <asp:Label ID="ControlFinder" runat="server"  Visible="false"></asp:Label>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td>
                                        投标采购任务列表:
                                        <asp:RadioButton ID="rad_all" runat="server" Text="全部单据" TextAlign="Right" GroupName="select00"
                                            OnCheckedChanged="rad_all_CheckedChanged" AutoPostBack="true"  />
                                        &nbsp;
                                        <asp:RadioButton ID="rad_mypart" runat="server" Text="我的任务" TextAlign="Right" GroupName="select00"
                                            OnCheckedChanged="rad_mypart_CheckedChanged" AutoPostBack="true"  Checked="true"/>&nbsp;
                                        <asp:TextBox ID="Tb_marrepcode" runat="server" Text="" Visible="false"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        状态:
                                        <asp:RadioButton ID="rad_stall" runat="server" Text="全部" TextAlign="Right" GroupName="select11"
                                            OnCheckedChanged="rad_stall_CheckedChanged" AutoPostBack="true"  />
                                        <asp:RadioButton ID="rad_stwzx" runat="server" Text="未执行" TextAlign="Right" GroupName="select11"
                                            OnCheckedChanged="rad_stwzx_CheckedChanged" AutoPostBack="true"  Checked="true"/>
                                        <asp:RadioButton ID="rad_stxdd" runat="server" Text="下订单" TextAlign="Right" GroupName="select11"
                                            OnCheckedChanged="rad_stxdd_CheckedChanged" AutoPostBack="true" Visible="false" />
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btn_toubiaoM" runat="server" Text="生成订单" 
                                            onclick="btn_toubiaoM_Click" />
                                        <asp:Button ID="btn_add" runat="server" Text="追加订单" OnClick="btn_add_Click" />&nbsp;&nbsp;    
                                        <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click"/>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div>
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        项目:
                                        <asp:TextBox ID="Tb_PJNAME" runat="server" OnTextChanged="Tb_PJNAME_Textchanged" AutoPostBack="true"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="Tb_PJNAME"
                                           ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                                           ServiceMethod="GetPJNAME" FirstRowSelected="true"  CompletionListCssClass="autocomplete_completionListElement"
                                         CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
                                        </cc1:AutoCompleteExtender>
                                        设备:
                                        <asp:TextBox ID="Tb_ENGNAME" runat="server" OnTextChanged="Tb_ENGNAME_Textchanged" AutoPostBack="true"></asp:TextBox>
                                         <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="Tb_ENGNAME"
                                           ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="20" MinimumPrefixLength="1" CompletionInterval="10"
                                           ServiceMethod="GetENGNAME" FirstRowSelected="true"  CompletionListCssClass="autocomplete_completionListElement"
                                          CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
                                        </cc1:AutoCompleteExtender>
                                        批号:
                                        <asp:TextBox ID="Tb_pcode" runat="server" Text=""></asp:TextBox>
                                        开始日期:<asp:TextBox ID="tb_riqif" runat="server" Text="" Width="110px"></asp:TextBox>
                                        结束日期:<asp:TextBox ID="tb_riqit" runat="server" Text="" Width="110px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tb_riqif"
                                            Format="yyyy-MM-dd">
                                        </cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tb_riqit"
                                            Format="yyyy-MM-dd">
                                        </cc1:CalendarExtender>
                                        采购员:<asp:DropDownList ID="drp_stu" runat="server">
                                        </asp:DropDownList>
                                       
                                        <asp:Button ID="btn_search" runat="server" Text="查询" OnClick="btn_search_click" />
                                        <asp:Button ID="btn_clear" runat="server" Text="清除" OnClick="btn_clear_click" />
                                         <asp:Button ID="btn_filter" runat="server" Text="过滤" OnClientClick="openmodewin()"/>
                                        <asp:TextBox ID="hid_filter" runat="server" Style="display: none"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="height: 470px; overflow: auto; width: 100%">
                            <div class="cpbox xscroll">
                                <table id="tab" class="nowrap cptable fullwidth" align="center">
                                    <asp:Repeater ID="tbpc_purshaseplanassignRepeater" runat="server" OnItemDataBound="tbpc_purshaseplanassignRepeaterbind">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color:#B9D3EE">
                                               <td>
                                               </td>
                                                <td>
                                                    <strong>行号</strong>
                                                </td>
                                                <td>
                                                    <strong>批号</strong>
                                                </td>
                                                <td>
                                                    <strong>项目</strong>
                                                </td>
                                                <td>
                                                    <strong>工程</strong>
                                                </td>
                                                <td>
                                                    <strong>计划号</strong>
                                                </td>
                                                <td>
                                                    <strong>材料编码</strong>
                                                </td>
                                                <td>
                                                    <strong>名称</strong>
                                                </td>
                                                <td>
                                                    <strong>图号</strong>
                                                </td>
                                                <td>
                                                    <strong>材质</strong>
                                                </td>
                                                <td>
                                                    <strong>规格</strong>
                                                </td>
                                                <td>
                                                    <strong>国标</strong>
                                                </td>
                                               
                                                <td>
                                                    <strong>供应商</strong>
                                                </td>
                                                <td>
                                                    <strong>单价</strong>
                                                </td>
                                                <td id="rate" visible="false" runat="server">
                                                    <strong>税率</strong>
                                                </td>
                                                <td>
                                                    <strong>采购数量</strong>
                                                </td>
                                                 <td>
                                                    <strong>单位</strong>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <strong>采购辅助数量</strong>
                                                </td>
                                                 <td runat="server" visible="false">
                                                    <strong>辅助单位</strong>
                                                </td>
                                                 <td id="fengongren" visible="false" runat="server">
                                                    <strong>分工人</strong>
                                                </td>
                                                <td id="fengongriqi"  visible="false" runat="server">
                                                    <strong>日期</strong>
                                                </td>
                                                <td id="caigouren" runat="server">
                                                    <strong>采购员</strong>
                                                </td>
                                                <td>
                                                    <strong>执行数量</strong>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <strong>执行辅助数量</strong>
                                                </td>
                                               
                                                <td runat="server" visible="false">
                                                    <strong>长度</strong>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <strong>宽度</strong>
                                                </td>
                                                 <td>
                                                    <strong>类型</strong>
                                                </td>
                                                <td>
                                                    <strong>执行人</strong>
                                                </td>
                                                <td>
                                                    <strong>执行时间</strong>
                                                </td> 
                                                
                                                <td>
                                                    <strong>是否生成订单</strong>
                                                </td>
                                                
                                                <td id="td1" runat="server" visible="false">
                                                    <strong>订单号</strong>
                                                </td>
                                                <td>
                                                    <strong>备注</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr id="row" runat="server" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)"
                                                ondblclick="redirectw(this)">
                                                <td>
                                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        Checked="false" onclick="checkme(this)"></asp:CheckBox>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="ROWSNUM" runat="server"  Text='<%# Container.ItemIndex + 1+(Convert.ToDouble(UCPaging1.CurrentPage)-1)*100%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_PCODE" runat="server" Text='<%#Eval("PUR_PCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                   
                                               
                                                </td>
                                               
                                                <td>
                                              
                                              
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("PUR_PTCODE")%>'></asp:Label>
                                                    <asp:Label ID="PUR_lbpurid" runat="server" Text='<%#Eval("pur_id")%>' Visible="false"></asp:Label>
                                             
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("PUR_MARID")%>'></asp:Label>
                                                
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("PUR_MARNAME")%>'></asp:Label>
                                                </td>
                                                 <td>
                                                    <asp:Label ID="PUR_TUHAO" runat="server" Text='<%#Eval("PUR_TUHAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("PUR_MARTERIAL")%>'></asp:Label>
                                               
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("PUR_MARNORM")%>'></asp:Label>
                                              
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("PUR_GUOBIAO")%>'></asp:Label>
                                                </td>
                                                
                                                <td>
                                                    <asp:Label ID="PUR_SUPPLY" runat="server" Text='<%#Eval("PUR_SUPPLY")%>' Visible="false"></asp:Label>
                                                     <asp:Label ID="PUR_SUPPLYNAME" runat="server" Text='<%#Eval("PUR_SUPPLYNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_PRICE" runat="server" Text='<%#Eval("PUR_PRICE")%>'></asp:Label>
                                                </td>
                                                <td id="taxrate" visible="false">
                                                    <asp:Label ID="PUR_TAXRATE" runat="server" Text='<%#Eval("PUR_TAXRATE")%>'></asp:Label>
                                                </td>
                                                
                                                <td>
                                                    <asp:Label ID="PUR_RPNUM" runat="server" Text='<%#Eval("PUR_RPNUM")%>'></asp:Label>
                                           
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("PUR_NUNIT")%>'></asp:Label>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <asp:Label ID="PUR_RPFZNUM" runat="server" Text='<%#Eval("PUR_RPFZNUM")%>'></asp:Label>
                                             
                                                </td>
                                                 <td runat="server" visible="false">                                                  
                                                    <asp:Label ID="FZUNIT" runat="server" Text='<%#Eval("FZUNIT")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td id="fenid" visible="false">
                                                    <asp:Label ID="PUR_PTASMAN" runat="server" Text='<%#Eval("PUR_PTASMAN")%>'></asp:Label>
                                          
                                                </td>
                                                <td id="fentime" visible="false">                                                    
                                                    <asp:Label ID="PUR_PTASTIME" runat="server" Text='<%#Eval("PUR_PTASTIME")%>'></asp:Label>
                                            
                                                </td>
                                                <td id="cai">                                                  
                                                    <asp:Label ID="PUR_CGMAN" runat="server" Text='<%#Eval("PUR_CGMAN")%>' Visible="false"></asp:Label>
                                       
                                                    <asp:Label ID="PUR_CGMANNM" runat="server" Text='<%#Eval("PUR_CGMANNM")%>'></asp:Label>
                                           
                                                </td>
                                                <td>                                                  
                                                    <asp:Label ID="ZXNUM" runat="server" Text='<%#Eval("ZXNUM")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td runat="server" visible="false">                                                  
                                                    <asp:Label ID="ZXFZNUM" runat="server" Text='<%#Eval("ZXFZNUM")%>'></asp:Label>&nbsp;
                                                </td>
                                               
                                                <td runat="server" visible="false">                                                   
                                                    <asp:Label ID="PUR_LENGTH" runat="server" Text='<%#Eval("PUR_LENGTH")%>'></asp:Label>
                                              
                                                </td>
                                                <td runat="server" visible="false">                                                   
                                                    <asp:Label ID="PUR_WIDTH" runat="server" Text='<%#Eval("PUR_WIDTH")%>'></asp:Label>
                                             
                                                </td>
                                                 <td>
                                                    <asp:Label ID="PUR_MASHAPE" runat="server" Text='<%#Eval("PUR_MASHAPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="ICL_REVIEWA" runat="server" Text='<%#Eval("ICL_REVIEWA")%>' Visible="false"></asp:Label>
                                                
                                                    <asp:Label ID="ICL_REVIEWANM" runat="server" Text='<%#Eval("ICL_REVIEWANM")%>'></asp:Label>
                                               
                                                </td>
                                                <td>
                                                    <asp:Label ID="ICL_IQRDATE" runat="server" Text='<%#Eval("ICL_IQRDATE")%>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:HyperLink ID="Hypdd" runat="server" Target="_blank">
                                                        <asp:Label ID="PUR_DD" runat="server" Text='<%#get_pur_dd(Eval("PUR_STATE").ToString())%>'></asp:Label></asp:HyperLink>
                                                    <asp:Label ID="PUR_STATE" runat="server" Text='<%#Eval("PUR_STATE")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="IB_STATE" runat="server" Text='<%#Eval("state")%>' Visible="false"></asp:Label>
                                                </td>
                                                
                                                <td id="td1" runat="server" visible="false">
                                                    <asp:Label ID="PO_SHEETNO" runat="server" Text='<%#Eval("PO_SHEETNO")%>'></asp:Label>
                                                </td>
                                                <td>                                                   
                                                    <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("PUR_NOTE")%>'></asp:Label>
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
	        cssSkin : "sDefault",
	        headerRows : 1,
	        fixedCols : 0,
//	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
	        onStart : function () 
	        {
	             var ptc="<%=gloabptc%>";
                if(ptc!="")
                {
                    var col=5;
                    var table=document.getElementById("tab"); 
                    var tr=table.getElementsByTagName("tr"); 
                    for( i=1;i <tr.length-1;i++) 
                    { 
                       if(tr[i].getElementsByTagName("td")[col].getElementsByTagName("span")[0].innerHTML==ptc)
                        { 
                            tr[i].style.backgroundColor ='Orange'; 
                           
                        }
                        else 
                        {
                            tr[i].style.backgroundColor ='';
                        }
                     }  
                 }      
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
                        <br />
                        <asp:Panel ID="Panel1" runat="server">
                            <table width="100%">
                                <tr>
                                    <td width="5%" align="left">
                                        <asp:CheckBox ID="selectall" runat="server" Text="全选" AutoPostBack="true" TextAlign="left"
                                            Checked="false" OnCheckedChanged="selectall_CheckedChanged" />
                                         <asp:Button ID="btn_LX" runat="server" Text="连选"  OnClick="btn_LX_click"/>
                                        <asp:Button ID="btn_QX" runat="server" Text="取消"  OnClick="btn_QX_click"/>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
