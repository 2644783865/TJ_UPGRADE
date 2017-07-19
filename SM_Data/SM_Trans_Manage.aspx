<%@ Page Title="���˹���" Language="C#" MasterPageFile="~/Masters/BaseMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="SM_Trans_Manage.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">���˹���
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

        
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>    
    
     <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
 
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers >       
           <asp:PostBackTrigger ControlID="Export2" />
           <asp:PostBackTrigger ControlID="Export4" />
        </Triggers>
        <ContentTemplate>  --%>
    
         
    <cc1:TabContainer runat="server" ID="TabContainer1" AutoPostBack="true" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
        
    <cc1:TabPanel runat="server" ID="Tab2" HeaderText="���ڼ���" >
    <ContentTemplate>
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
                &nbsp;&nbsp;&nbsp;                
                ��ݣ�<asp:DropDownList ID="DropDownListYear2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListYear2_SelectedIndexChanged">
               </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Button ID="Add2" runat="server" Text="���" OnClick="Add2_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Delete2" runat="server" Text="ɾ��" OnClick="Delete2_Click"  OnClientClick="javascript:return confirm('ȷ��ɾ����');"/>&nbsp;&nbsp;&nbsp;  
                <asp:Button ID="Export2" runat="server" Text="����" OnClick="Export2_Click" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    <asp:Panel ID="PanelCNJG" runat="server"  style="width:100%; height:400px; overflow:scroll; overflow-y:auto; overflow-x:yes; display:block;">
    <div style="width:1700px">
    <table id="cnjg" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="RepeaterCNJG" runat="server" OnItemDataBound="RepeaterCNJG_ItemDataBound">
            <HeaderTemplate>
            <tr align="center" class="tableTitle">
                <td colspan="20"><strong><asp:Label ID="LabelYear2" runat="server"></asp:Label>�꼯�۷���̨��</strong></td>                          
            </tr>
            <tr>
                <td rowspan="2"><strong></strong></td>
                <td rowspan="2"><strong>���</strong></td>
                <td rowspan="2"><strong>����/����</strong></td>
                <td rowspan="2"><strong>Ŀ�ĸ�</strong></td>
                <td rowspan="2"><strong>�������رȣ�������/�֣�</strong></td>
                <td><strong>��ͬ</strong></td>
                <td colspan="7"><strong>����</strong></td>
                <td colspan="3"><strong>��������</strong></td>
                <td rowspan="2"><strong>�вĽ��裨�֣�</strong></td>
                <td rowspan="2"><strong>�вĽ��貿�ֽ��</strong></td>
                <td rowspan="2"><strong>��ע</strong></td>
                <td rowspan="2"><strong></strong></td>
            </tr>
            <tr>
                <td><strong>��ͬ���</strong></td>
                <td><strong>��ʼ����</strong></td>
                <td><strong>��������</strong></td>
                <td><strong>�Ǵ��������T��</strong></td>
                <td><strong>���������T��</strong></td>
                <td><strong>�������m3��</strong></td>
                <td><strong>��������T��</strong></td>
                <td><strong>���Σ�T��</strong></td>                   
                <td><strong>�Ǵ��������T��</strong></td>
                <td><strong>�������</strong></td>                   
                <td><strong>��Ԫ��</strong></td>                                                
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>                
                <td><%#Container.ItemIndex+1%><asp:Label ID="LabelID" runat="server" Text='<%#Eval("CNJGID")%>' Visible="false"></asp:Label></td>
                <td><%#Eval("CNJGCC")%></td>
                <td><%#Eval("CNJGMDG")%></td>
                <td><%#Eval("CNJGRZB")%></td>                
                <td><%#Eval("CNJGHTH")%></td>
                <td><%#Eval("CNJGLLSTARTDATE")%></td>
                <td><%#Eval("CNJGLLENDDATE")%></td>
                <td><%#Eval("CNJGLLFDJZL")%></td>
                <td><%#Eval("CNJGLLDJZL")%></td>
                <td><%#Eval("CNJGLLZTJ")%></td>
                <td><%#Eval("CNJGLLZZL")%></td>
                <td><%#Eval("CNJGLLCC")%></td>
                <td><%#Eval("CNJGGBFDJZL")%></td>
                <td><%#Eval("CNJGGBDJZL")%></td>
                <td><%#Eval("CNJGGBJE")%></td>
                <td><%#Eval("CNJGZCJSZL")%></td>
                <td><%#Eval("CNJGZCJSJE")%></td>
                <td><%#Eval("CNJGBZ")%></td>                
                <td><asp:HyperLink ID="HyperLinkEditCNJG" NavigateUrl='<%#"SM_Trans_CNJGEdit.aspx?FLAG=EDIT&&ID="+Eval("CNJGID")%>'  runat="server"><asp:Image ID="ImageModify" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />�޸�</asp:HyperLink></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle">
                <td></td>                
                <td><strong>�ϼƣ�</strong></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>           
            </tr>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="Panel2" runat="server" Visible="false">û����ؼ�¼!</asp:Panel>
    </table>
    </div>
    </asp:Panel>
    </div><!--box-outer END -->
    </div> <!--box-wrapper END --> 
    </ContentTemplate>
    </cc1:TabPanel>
    
 
    <cc1:TabPanel runat="server" ID="Tab4" HeaderText="���ڷ���">
    <ContentTemplate>
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
                &nbsp;&nbsp;&nbsp;
                ��ݣ�<asp:DropDownList ID="DropDownListYear4" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListYear4_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Button ID="Add4" runat="server" Text="���" OnClick="Add4_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Delete4" runat="server" Text="ɾ��" OnClick="Delete4_Click" OnClientClick="javascript:return confirm('ȷ��ɾ����');" />&nbsp;&nbsp;&nbsp;                
                <asp:Button ID="Export4" runat="server" Text="����" OnClick="Export4_Click" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    <asp:Panel ID="PanelGNFY" runat="server"  style="overflow:auto;position:static" Width="100%" Height="400px">
    <div style="width:1700px">
    <table id="gnfy" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="RepeaterGNFY" runat="server" OnItemDataBound="RepeaterGNFY_ItemDataBound">
            <HeaderTemplate>
            <tr align="center" class="tableTitle">
                <td colspan="20"><strong><asp:Label ID="LabelYear4" runat="server" ></asp:Label>������豸����̨��</strong></td>                          
            </tr>
            <tr>
                <td rowspan="2"><strong></strong></td>
                <td rowspan="2"><strong>���</strong></td>
                <td rowspan="2"><strong>��Ŀ����</strong></td>
                <td colspan="2"><strong>�����ͬ</strong></td>
                <td rowspan="2"><strong>�������رȣ�������/�֣�</strong></td>
                <td colspan="7"><strong>����</strong></td>
                <td colspan="2"><strong>��������</strong></td>
                <td rowspan="2"><strong>���ս����Ԫ��</strong></td>
                <td rowspan="2"><strong>�вĽ��裨�֣�</strong></td>
                <td rowspan="2"><strong>�вĽ��貿�ֽ��</strong></td>
                <td rowspan="2"><strong>��ע</strong></td>
                <td rowspan="2"><strong></strong></td>
            </tr>
            <tr>
                <td><strong>��ͬ���</strong></td>
                <td><strong>��ͬ���</strong></td>
                <td><strong>��ʼ����</strong></td>
                <td><strong>��������</strong></td>
                <td><strong>�Ǵ��������T��</strong></td>
                <td><strong>���������T��</strong></td>
                <td><strong>�������m3��</strong></td>
                <td><strong>��������T��</strong></td>
                <td><strong>���Σ�T��</strong></td>                   
                <td><strong>�Ǵ��������T��</strong></td>
                <td><strong>�������</strong></td>                                                                 
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>                
                <td><%#Container.ItemIndex+1%><asp:Label ID="LabelID" runat="server" Text='<%#Eval("GNFYID")%>' Visible="false"></asp:Label></td>
                <td><%#Eval("GNFYPROJECT")%></td>
                <td><%#Eval("GNFYHTBH")%></td>
                <td><%#Eval("GNFYHTJE")%></td>                
                <td><%#Eval("GNFYRZB")%></td>
                <td><%#Eval("GNFYLLSTARTDATE")%></td>
                <td><%#Eval("GNFYLLENDDATE")%></td>
                <td><%#Eval("GNFYLLFDJZL")%></td>
                <td><%#Eval("GNFYLLDJZL")%></td>
                <td><%#Eval("GNFYLLZTJ")%></td>
                <td><%#Eval("GNFYLLZZL")%></td>
                <td><%#Eval("GNFYLLCC")%></td>
                <td><%#Eval("GNFYGBFDJZL")%></td>
                <td><%#Eval("GNFYGBDJZL")%></td>
                <td><%#Eval("GNFYZZJSJE")%></td>
                <td><%#Eval("GNFYZCJSZL")%></td>
                <td><%#Eval("GNFYZCJSJE")%></td>
                <td><%#Eval("GNFYBZ")%></td>                
                <td><asp:HyperLink ID="HyperLinkEditGNFY" NavigateUrl='<%#"SM_Trans_GNFYEdit.aspx?FLAG=EDIT&&ID="+Eval("GNFYID")%>'  runat="server"><asp:Image ID="ImageModify" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />�޸�</asp:HyperLink></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle">
                <td></td>                
                <td><strong>�ϼƣ�</strong></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>           
            </tr>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="Panel4" runat="server" Visible="false">û����ؼ�¼!</asp:Panel>
    </table>
    </div>
    </asp:Panel>
    </div><!--box-outer END -->
    </div> <!--box-wrapper END --> 
    </ContentTemplate>
    </cc1:TabPanel>

    <cc1:TabPanel runat="server" ID="Tab5" HeaderText="�ͻ�����">
  <ContentTemplate>
<div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
                &nbsp;&nbsp;&nbsp;
                ��ݣ�<asp:DropDownList ID="DropDownListYear5" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListYear5_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Button ID="Add5" runat="server" Text="���"   OnClick="Add5_Click"  />&nbsp;&nbsp;&nbsp;                
                <asp:Button ID="Delete5" runat="server" Text="ɾ��" OnClick="Delete5_Click"  OnClientClick="javascript:return confirm('ȷ��ɾ����');" />&nbsp;&nbsp;&nbsp;    
                <asp:Button ID="Export5" runat="server" Text="����" OnClick="Export5_Click" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    <asp:Panel ID="Panel1" runat="server"  style="overflow:auto;position:static" Width="100%" Height="400px">
    <table id="Table1" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="RepeaterYZZT" runat="server" OnItemDataBound="RepeaterYZZT_ItemDataBound" >
            <HeaderTemplate>
            <tr align="center" class="tableTitle">
                <td colspan="7" align="center"><strong><asp:Label ID="LabelYear5" runat="server" ></asp:Label>ҵ����������̨��</strong></td>                          
            </tr>
            <tr>
                <td><strong></strong></td>
                <td width="30px"><strong>���</strong></td>
                <td><strong>����</strong></td>
                <td><strong>��������</strong></td>              
                <td><strong>����(��)</strong></td>
                <td><strong>����������ף�</strong></td>         
                <td><strong>��ע</strong></td>               
            </tr>
           
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>
                <td width="30px" ><%#Container.ItemIndex+1%><asp:Label ID="LabelID" runat="server" Text='<%#Eval("KHZT_ID")%>' Visible="false"></asp:Label></td>
                <td><%#Eval("KHZT_DATE")%></td>
                <td><%#Eval("KHZT_NAME")%></td>
                <td><%#Eval("KHZT_NUM")%></td>                
                <td><%#Eval("KHZT_LFM")%></td>
                <td><%#Eval("KHZT_BZ")%></td>              
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle">
                <td></td>
                <td><strong>�ϼƣ�</strong></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
               
            </tr>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="Panel5" runat="server" Visible="false">û����ؼ�¼!</asp:Panel>
    </table>
    </asp:Panel>
    </div><!--box-outer END -->
    </div> <!--box-wrapper END --> 
 </ContentTemplate>
    </cc1:TabPanel>

  

    <cc1:TabPanel runat="server" ID="Tab7" HeaderText="����">
    <ContentTemplate>
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
                &nbsp;&nbsp;&nbsp;
                ��ݣ�<asp:DropDownList ID="DropDownListYear7" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListYear7_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Button ID="Add7" runat="server" Text="���" OnClick="Add7_Click" />&nbsp;&nbsp;&nbsp;                
                <asp:Button ID="Delete7" runat="server" Text="ɾ��" OnClick="Delete7_Click" OnClientClick="javascript:return confirm('ȷ��ɾ����');" />&nbsp;&nbsp;&nbsp;                     
                <asp:Button ID="Export7" runat="server" Text="����" OnClick="Export7_Click" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    <asp:Panel ID="PanelKY" runat="server"  style="overflow:auto;position:static" Width="100%" Height="400px">
    <div style="width:1200px">
    <table id="ky" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="RepeaterKY" runat="server" OnItemDataBound="RepeaterKY_ItemDataBound">
            <HeaderTemplate>
            <tr align="center" class="tableTitle">
                <td colspan="15"><strong><asp:Label ID="LabelYear7" runat="server" ></asp:Label>�������˷���̨��</strong></td>                          
            </tr>
            <tr>
                <td><strong></strong></td>
                <td><strong>���</strong></td>
                <td><strong>��Ŀ����</strong></td>
                <td><strong>��������</strong></td>
                <td><strong>����</strong></td>
                <td><strong>��װ��ʽ</strong></td>
                <td><strong>�����m3��</strong></td>
                <td><strong>������KG��</strong></td>
                <td><strong>�˷ѣ�Ԫ��</strong></td>
                <td><strong>���乫˾</strong></td>
                <td><strong>��������</strong></td>
                <td><strong>������</strong></td>
                <td><strong>��ע</strong></td>
                <td><strong>�˷ѽ������</strong></td>
                <td ><strong></strong></td>
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>
                <td><%#Container.ItemIndex+1%><asp:Label ID="LabelID" runat="server" Text='<%#Eval("KYID")%>' Visible="false"></asp:Label></td>
                <td><%#Eval("KYPROJECT")%></td>
                <td><%#Eval("KYGOODNAME")%></td>
                <td><%#Eval("KYNUM")%></td>                
                <td><%#Eval("KYBZXS")%></td>
                <td><%#Eval("KYTJ")%></td>
                <td><%#Eval("KYZL")%></td>
                <td><%#Eval("KYYF")%></td>
                <td><%#Eval("KYYSGS")%></td>
                <td><%#Eval("KYTRANSDATE")%></td>
                <td><%#Eval("KYFYR")%></td>
                <td><%#Eval("KYBZ")%></td>
                <td><%#Eval("KYYFJSQK")%></td>
                <td><asp:HyperLink ID="HyperLinkEditKY" NavigateUrl='<%#"SM_Trans_KYEdit.aspx?FLAG=EDIT&&ID="+Eval("KYID")%>'  runat="server"><asp:Image ID="ImageModify" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />�޸�</asp:HyperLink></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle">
                <td></td>                
                <td><strong>�ϼƣ�</strong></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>                       
            </tr>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="Panel7" runat="server" Visible="false">û����ؼ�¼!</asp:Panel>
    </table>
    </div>
    </asp:Panel>
    </div><!--box-outer END -->
    </div> <!--box-wrapper END --> 
    </ContentTemplate>
    </cc1:TabPanel>
    
    <cc1:TabPanel runat="server" ID="Tab8" HeaderText="�㵣����">
    <ContentTemplate>
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
                &nbsp;&nbsp;&nbsp;
                ��ݣ�<asp:DropDownList ID="DropDownListYear8" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListYear8_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Button ID="Add8" runat="server" Text="���" OnClick="Add8_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Delete8" runat="server" Text="ɾ��" OnClick="Delete8_Click"  OnClientClick="javascript:return confirm('ȷ��ɾ����');"/>&nbsp;&nbsp;&nbsp;                                
                <asp:Button ID="Export8" runat="server" Text="����" OnClick="Export8_Click" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    <asp:Panel ID="PanelLDHY" runat="server"  style="overflow:auto;position:static" Width="100%" Height="400px">
    <div style="width:1200px">
    <table id="ldhy" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="RepeaterLDHY" runat="server" OnItemDataBound="RepeaterLDHY_ItemDataBound">
            <HeaderTemplate>
            <tr align="center" class="tableTitle">
                <td colspan="16"><strong><asp:Label ID="LabelYear8" runat="server" ></asp:Label>���㵣���˼�¼̨��</strong></td>                          
            </tr>
            <tr>
                <td><strong></strong></td>            
                <td><strong>���</strong></td>
                <td><strong>��Ŀ����</strong></td>
                <td><strong>��������</strong></td>
                <td><strong>����</strong></td>
                <td><strong>��װ��ʽ</strong></td>
                <td><strong>�����m3��</strong></td>
                <td><strong>������KG��</strong></td>
                <td><strong>Ӧ���˷ѣ�Ԫ��</strong></td>
                <td><strong>Ӧ���˷ѣ�Ԫ��</strong></td>                
                <td><strong>���䷽ʽ</strong></td>
                <td><strong>��������</strong></td>
                <td><strong>������</strong></td>
                <td><strong>��ע</strong></td>
                <td><strong>�˷ѽ������</strong></td>
                <td ><strong></strong></td>
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>                
                <td><%#Container.ItemIndex+1%><asp:Label ID="LabelID" runat="server" Text='<%#Eval("LDHYID")%>' Visible="false"></asp:Label></td>
                <td><%#Eval("LDHYPROJECT")%></td>
                <td><%#Eval("LDHYGOODNAME")%></td>
                <td><%#Eval("LDHYNUM")%></td>                
                <td><%#Eval("LDHYBZXS")%></td>
                <td><%#Eval("LDHYTJ")%></td>
                <td><%#Eval("LDHYZL")%></td>
                <td><%#Eval("LDHYYFYF")%></td>
                <td><%#Eval("LDHYYSYF")%></td>
                <td><%#Eval("LDHYYSFS")%></td>
                <td><%#Eval("LDHYTRANSDATE")%></td>
                <td><%#Eval("LDHYCZR")%></td>
                <td><%#Eval("LDHYBZ")%></td>
                <td><%#Eval("LDHYYFJSQK")%></td>
                <td><asp:HyperLink ID="HyperLinkEditLDHY" NavigateUrl='<%#"SM_Trans_LDHYEdit.aspx?FLAG=EDIT&&ID="+Eval("LDHYID")%>'  runat="server"><asp:Image ID="ImageModify" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />�޸�</asp:HyperLink></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle">
                <td></td>
                <td><strong>�ϼƣ�</strong></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td> 
                <td></td>                       
            </tr>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="Panel8" runat="server" Visible="false">û����ؼ�¼!</asp:Panel>
    </table>
    </div>
    </asp:Panel>
    </div><!--box-outer END -->
    </div> <!--box-wrapper END --> 
    </ContentTemplate>
    </cc1:TabPanel>

    <cc1:TabPanel runat="server" ID="Tab9" HeaderText="��װ�䷢��">
    <ContentTemplate>
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
                &nbsp;&nbsp;&nbsp;
                ��ݣ�<asp:DropDownList ID="DropDownListYear9" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListYear9_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Button ID="Add9" runat="server" Text="���" OnClick="Add9_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Delete9" runat="server" Text="ɾ��" OnClick="Delete9_Click" OnClientClick="javascript:return confirm('ȷ��ɾ����');" />&nbsp;&nbsp;&nbsp;                                
                <asp:Button ID="Export9" runat="server" Text="����" OnClick="Export9_Click" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    <asp:Panel ID="PanelJZXFY" runat="server"  style="overflow:auto;position:static" Width="100%" Height="400px">
    <div style="width:1200px">
    <table id="jzxfy" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="RepeaterJZXFY" runat="server" OnItemDataBound="RepeaterJZXFY_ItemDataBound">
            <HeaderTemplate>
            <tr align="center" class="tableTitle">
                <td colspan="16"><strong>�в��ػ����˲�<asp:Label ID="LabelYear9" runat="server" ></asp:Label>��ȼ�װ�䷢��ͳ��</strong></td>                          
            </tr>
            <tr>
                <td rowspan="2"><strong></strong></td>
                <td rowspan="2"><strong>���</strong></td>
                <td rowspan="2"><strong>��Ŀ����</strong></td>
                <td rowspan="2"><strong>��������</strong></td>
                <td rowspan="2"><strong>��������</strong></td>
                <td rowspan="2"><strong>��������</strong></td>
                <td colspan="3"><strong>װ����</strong></td>
                <td rowspan="2"><strong>���ر�</strong></td>
                <td rowspan="2"><strong>���װ����</strong></td>
                <td rowspan="2"><strong>����װ����</strong></td>
                <td rowspan="2"><strong>���ͼ�����</strong></td>
                <td rowspan="2"><strong>װ�����ò���</strong></td>
                <td rowspan="2"><strong></strong></td>                
                <td rowspan="2"><strong></strong></td>
            </tr>
            <tr>
                <td><strong>����</strong></td>
                <td><strong>������</strong></td>
                <td><strong>���أ�T��</strong></td>  
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>                
                <td><%#Container.ItemIndex+1%><asp:Label ID="LabelID" runat="server" Text='<%#Eval("JZXFYID")%>' Visible="false"></asp:Label></td>
                <td><%#Eval("JZXFYPROJECT")%></td>
                <td><%#Eval("JZXFYFYPC")%></td>
                <td><%#Eval("JZXFYTRANSDATE")%></td>                
                <td><%#Eval("JZXFYHWMS")%></td>
                <td><%#Eval("JZXFYXS")%></td>
                <td><%#Eval("JZXFYLFM")%></td>
                <td><%#Eval("JZXFYHZ")%></td>
                <td><%#Eval("JZXFYRZB")%></td>
                <td><%#Eval("JZXFYTJZXL")%></td>
                <td><%#Eval("JZXFYZLZXL")%></td>
                <td><%#Eval("JZXFYXXJXS")%></td>
                <td><%#Eval("JZXFYZXSYCL")%></td>
                <td><asp:HyperLink ID="HyperLinkEditJZXFYMX" NavigateUrl='<%#"SM_Trans_JZXFYMXEdit.aspx?FLAG=NEW&&ID=NEW&&PID="+Eval("JZXFYID")%>'  runat="server"><asp:Image ID="ImageAddDetail" ImageUrl="~/assets/icons/add.gif" border="0" hspace="2" align="absmiddle" runat="server" />�����ϸ</asp:HyperLink></td>
                <td><asp:HyperLink ID="HyperLinkEditJZXFY" NavigateUrl='<%#"SM_Trans_JZXFYEdit.aspx?FLAG=EDIT&&ID="+Eval("JZXFYID")%>'  runat="server"><asp:Image ID="ImageModify" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />�޸�</asp:HyperLink></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle">
                <td></td>
                <td><strong>�ϼƣ�</strong></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>           
            </tr>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="Panel9" runat="server" Visible="false">û����ؼ�¼!</asp:Panel>
    </table>
    </div>
    </asp:Panel>
    </div><!--box-outer END -->
    </div> <!--box-wrapper END --> 
    </ContentTemplate>
    </cc1:TabPanel>
    
    <cc1:TabPanel runat="server" ID="Tab10" HeaderText="��װ�䷢������ϸ">
    <ContentTemplate>
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
                &nbsp;&nbsp;&nbsp;
                ��ݣ�<asp:DropDownList ID="DropDownListYear10" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListYear10_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Button ID="Delete10" runat="server" Text="ɾ��" OnClick="Delete10_Click" OnClientClick="javascript:return confirm('ȷ��ɾ����');" />&nbsp;&nbsp;&nbsp;                
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    <asp:Panel ID="PanelJZXFYMX" runat="server"  style="overflow:auto;position:static" Width="100%" Height="400px">
    <table id="jzxfymx" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="RepeaterJZXFYMX" runat="server" OnItemDataBound="RepeaterJZXFYMX_ItemDataBound">
            <HeaderTemplate>
            <tr align="center" class="tableTitle">
                <td colspan="9"><strong>��װ�䷢������ϸ</strong></td>                          
            </tr>
            <tr>
                <td><strong></strong></td>            
                <td><strong>���</strong></td>
                <td><strong>��Ŀ����</strong></td>
                <td><strong>��������</strong></td>
                <td><strong>��������</strong></td>
                <td><strong>��������</strong></td>
                <td><strong>�����ƺ�</strong></td>
                <td><strong>����������KG��</strong></td>
                <td><strong></strong></td>
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>                
                <td><%#Container.ItemIndex+1%><asp:Label ID="LabelID" runat="server" Text='<%#Eval("JZXFYMXID")%>' Visible="false"></asp:Label></td>
                <td><%#Eval("JZXFYPROJECT")%></td>
                <td><%#Eval("JZXFYFYPC")%></td>
                <td><%#Eval("JZXFYTRANSDATE")%></td>                
                <td><%#Eval("JZXFYMXGOODNAME")%></td>
                <td><%#Eval("JZXFYMXSCZH")%></td>
                <td><%#Eval("JZXFYMXHWZL")%></td>
                <td><asp:HyperLink ID="HyperLinkEditJZXFYMX" NavigateUrl='<%#"SM_Trans_JZXFYMXEdit.aspx?FLAG=EDIT&&ID="+Eval("JZXFYMXID")%>'  runat="server"><asp:Image ID="ImageModify" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />�޸�</asp:HyperLink></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle">
                <td></td>                
                <td><strong>�ϼƣ�</strong></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="Panel10" runat="server" Visible="false">û����ؼ�¼!</asp:Panel>
    </table>
    </asp:Panel>
    </div><!--box-outer END -->
    </div> <!--box-wrapper END --> 
    </ContentTemplate>
    </cc1:TabPanel>

    </cc1:TabContainer>
    
    <%-- </ContentTemplate>
      </asp:UpdatePanel>  --%>
</asp:Content>
